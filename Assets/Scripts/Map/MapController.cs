using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapController : MonoBehaviour
{
    RoomController roomController;
    MapModel mapModel;
    PlayerModel playerModel;
    RoomView roomView;

    bool startGameLoop = false;
    bool isLocked = false;

    void Start()
    {           
        roomController = GetComponent<RoomController>();
        mapModel = GetComponent<MapModel>();
        roomView = GetComponent<RoomView>();

        ref GameObject player = ref mapModel.Player();        
        ref GameObject[] docks = ref mapModel.Docks();
        ref AudioClip backMusic = ref mapModel.BackMusic();
        ref AudioSource audioSource = ref mapModel.AudioSource();

        playerModel = player.GetComponent<PlayerModel>();

        ref int[,] level = ref mapModel.Level();
        ref int width = ref mapModel.Width();
        ref int height = ref mapModel.Height();

        level = new int[height, width];

        //Generate seed map which has random values in each cell to act as obstacles
        GenerateDistanceMap(0, 0);
        RandomValues();        
        FindPath(4, 4);
        GenerateWorld();
        docks = GameObject.FindGameObjectsWithTag("Dock");
        audioSource.clip = backMusic;
        audioSource.Play();
        startGameLoop = true;
        
    }

    void Update(){        
        if(startGameLoop){
            ref GameObject[] enemies = ref mapModel.Enemies();
            ref string playerRoom = ref playerModel.CurrentRoom();
            ref List<Tuple<string, bool>> rooms = ref mapModel.Rooms(); 
            ref AudioClip bossMusic = ref mapModel.BossMusic();
            ref AudioSource audioSource = ref mapModel.AudioSource();

            var room = Tuple.Create(playerRoom, false);          
            
            if(rooms.Contains(room) != false){
                if(!isLocked){
                    isLocked = true;
                    Debug.Log("Locked");
                    LockRoom();
                    if(playerRoom == "44"){
                        SpawnBoss();
                        audioSource.clip = bossMusic;
                        audioSource.Play();
                    }else{
                        SpawnEnemies();
                    }                    
                }else{
                    //Room is currently locked so check all enemies arent dead
                    if(GameObject.FindWithTag("Enemy") == null){
                        Debug.Log("Unlocked");
                        //All enemies have been killed
                        isLocked = false;
                        UnlockRoom();
                        rooms.Remove(room);
                        room = Tuple.Create(playerRoom, true);
                        rooms.Add(room);
                    }
                }
            }
        }        
    }

    void SpawnBoss(){        
        ref string playerRoom = ref playerModel.CurrentRoom();
        ref GameObject bossEnemy = ref mapModel.BossEnemy();
        int row = int.Parse(playerRoom[0].ToString());
        int col = int.Parse(playerRoom[1].ToString());

        Vector3Int roomCenter = new Vector3Int(col*50 + 20, row*50 + 20, 0);  
        GameObject newBossEnemy = Instantiate(bossEnemy, roomView.Collision().GetCellCenterWorld(roomCenter), Quaternion.identity);
    }

    void SpawnEnemies(){
        ref GameObject enemy = ref mapModel.Enemy();
        ref GameObject[] enemies = ref mapModel.Enemies();
        ref string playerRoom = ref playerModel.CurrentRoom();
        ref System.Random rand = ref mapModel.Rand();

        //Get Room center
        int row = int.Parse(playerRoom[0].ToString());
        int col = int.Parse(playerRoom[1].ToString());

        Vector3Int roomCenter = new Vector3Int(col*50 + 10, row*50 + 10, 0);     
        int numberOfEnemies = 1;//rand.Next(3, 7);    

        for(var i=0; i<numberOfEnemies; i++){
            //Generate random position in room            
            Vector3Int spawnPos = new Vector3Int(rand.Next(1, 20), rand.Next(1, 20), 0);
            spawnPos += roomCenter;
            GameObject newEnemy = Instantiate(enemy, roomView.Collision().GetCellCenterWorld(spawnPos), Quaternion.identity);
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
    }

    void LockRoom(){
        ref GameObject[] docks = ref mapModel.Docks();
        foreach(GameObject dock in docks){
            dock.GetComponent<DockController>().LockDock();
        }
    }

    void UnlockRoom(){
        ref GameObject[] docks = ref mapModel.Docks();
        foreach(GameObject dock in docks){
            dock.GetComponent<DockController>().UnlockDock();
        }
    }

    void GenerateWorld(){
        ref int[,] level = ref mapModel.Level();
        ref int width = ref mapModel.Width();
        ref int height = ref mapModel.Height();
        ref List<Tuple<string, bool>> rooms = ref mapModel.Rooms();

        rooms = new List<Tuple<string, bool>>();
        Vector3Int pos;
        for(var col=0; col< width; col++){
            for(var row=0; row<height; row++){
                if(level[row, col] == -1){
                    pos = new Vector3Int(col*50, row*50, 0);
                    roomController.GenerateRoom(pos);    
                    var curRoom = Tuple.Create(row.ToString() + col.ToString(), false);
                    Debug.Log(curRoom);
                    rooms.Add(curRoom);

                    if(row != height-1){
                        if(level[row+1, col] == -1){
                            roomController.AddUpDock(col, row);
                        }
                    }   
                    if(row != 0){                    
                        if(level[row-1, col] == -1){
                            roomController.AddDownDock(col, row);
                        }
                    }  
                    if(col != 0){
                        if(level[row, col-1] == -1){
                            roomController.AddLeftDock(col, row);
                        }
                    }
                    if(col != width-1){
                        if(level[row, col+1] == -1){
                            roomController.AddRightDock(col, row);
                        }
                    }
                }                
            }
        }
    }

    void FindPath(int startx, int starty){
        ref int[,] level = ref mapModel.Level();
        ref int width = ref mapModel.Width();
        ref int height = ref mapModel.Height();
        
        int currentx = startx;
        int currenty = starty;
        bool finished = false;
        level[currentx, currenty] = -1;
        while(!finished){
            int min = 999999;
            int minx = 999999;
            int miny = 9999999;
            if(currentx != height-1 && !finished){
                if(level[currentx+1, currenty] != -1){
                    if(level[currentx+1, currenty] == 0){
                        finished = true;
                        minx = currentx+1;
                        miny = currenty;
                        min = level[currentx+1, currenty];
                    }else if(level[currentx+1, currenty] < min){
                        minx = currentx+1;
                        miny = currenty;
                        min = level[currentx+1, currenty];
                    }
                }
            }
            if(currentx != 0 && !finished){
                if(level[currentx-1, currenty] != -1){
                    if(level[currentx-1, currenty] == 0){
                        finished = true;
                        minx = currentx-1;
                        miny = currenty;
                        min = level[currentx-1, currenty];
                    }else if(level[currentx-1, currenty] < min){
                        minx = currentx-1;
                        miny = currenty;
                        min = level[currentx-1, currenty];
                    }
                }
            }
            if(currenty != width-1 && !finished){
                if(level[currentx, currenty+1] != -1){
                    if(level[currentx, currenty+1] == 0){
                        finished = true;
                        minx = currentx;
                        miny = currenty+1;
                        min = level[currentx, currenty+1];
                    }else if(level[currentx, currenty+1] < min){
                        minx = currentx;
                        miny = currenty+1;
                        min = level[currentx, currenty+1];
                    }
                }
            }
            if(currenty != 0 && !finished){
                if(level[currentx, currenty-1] != -1){
                    if(level[currentx, currenty-1] == 0){
                        finished = true;
                        minx = currentx;
                        miny = currenty-1;
                        min = level[currentx, currenty-1];
                    }else if(level[currentx, currenty-1] < min){
                        minx = currentx;
                        miny = currenty-1;
                        min = level[currentx, currenty-1];
                    }
                }
            }

            currentx = minx;
            currenty = miny;
            level[minx, miny] = -1;
            
        }
    }

    void RandomValues()
    {        
        ref int[,] level = ref mapModel.Level();
        ref int width = ref mapModel.Width();
        ref int height = ref mapModel.Height();
        ref int randomVal = ref mapModel.RandomVal();
        ref System.Random rand = ref mapModel.Rand();
        //Randomise the values on the map
        for(var col = 0; col < width; col++)
        {
            for(var row = 0; row < height; row++)
            {   
                if(level[row, col] != 0){
                    randomVal = rand.Next(5, 10);
                    level[row, col] += randomVal;
                }
            }
        }
    }

    void Print(){
        ref int[,] level = ref mapModel.Level();
        ref int width = ref mapModel.Width();
        ref int height = ref mapModel.Height();
        for(var col=0 ; col<width; col++){
            string line = "";
            for(var row=0; row<height; row++){
                line = line + level[row, col] + " ";
            }
            Debug.Log(line);
        }
    }

    void GenerateDistanceMap(int endx, int endy){
        ref int[,] level = ref mapModel.Level();
        ref int width = ref mapModel.Width();
        ref int height = ref mapModel.Height();

        //Initialise Array with values
        for(var col = 0; col < width; col++){
            for(var row = 0; row < height; row++){   
                level[row, col] = -1;
            }
        }
        bool finished = false;
        level[endx, endy] = 0;
        int turn = 1;
        while(!finished){
            for(var col = 0; col<width; col++){
                for(var row = 0; row<height; row++){
                    if(level[row, col] != turn && level[row, col] != -1){
                        if(row != height-1){
                            if(level[row+1, col] == -1){
                                level[row+1, col] = turn;
                            }
                        }
                        if(row != 0){
                            if(level[row-1, col] == -1){
                                level[row-1, col] = turn;
                            }
                        }
                        if(col != width-1){
                            if(level[row, col+1] == -1){
                                level[row, col+1] = turn;
                            }
                        }
                        if(col != 0){
                            if(level[row, col-1] == -1){
                                level[row, col-1] = turn;
                            }
                        }
                    }
                }
            }
            for(var col = 0; col < width; col++){
                finished = true;
                for(var row = 0; row < height; row++){   
                    if(level[row, col] == -1){
                        finished = false;
                    }
                }
            }
            turn++;
        }
    }
}
