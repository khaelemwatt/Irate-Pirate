using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomController : MonoBehaviour
{
    RoomView roomView;
    RoomModel roomModel;

    public void GenerateRoom(Vector3Int startPosition)
    {
        roomView = gameObject.GetComponent<RoomView>();
        roomModel = gameObject.GetComponent<RoomModel>();

        ref Vector3Int startPos = ref roomModel.StartPos();
        startPos = startPosition;

        ref int[,] island = ref roomModel.Island();
        ref int[,] newIsland = ref roomModel.NewIsland();

        ref int width = ref roomModel.Width();
        ref int height = ref roomModel.Height();

        island = new int[height, width];  
        newIsland = new int[height, width]; 

        randomValues();  
        buildIsland();
        StartCoroutine(generateMap());       
    }

    IEnumerator generateMap()
    {
        for(int i=0; i<34; i++)
        {
            generateIslandMap();
            buildIsland();
        }
        for(int i=0; i<5; i++)
        {
            cleanupOcean();
            cleanupGrass();
        }

        generateGrassDetails();    
        generateBorder();
        yield return new WaitForSeconds(1f);
               
    }

    void randomValues()
    {
        ref int width = ref roomModel.Width();
        ref int height = ref roomModel.Height();
        ref System.Random rand = ref roomModel.Rand();
        ref int[,] island = ref roomModel.Island();
        ref float spawnChance = ref roomModel.SpawnChance();
        //Randomise the values on the map
        for(var col = 0; col < width; col++)
        {
            for(var row = 0; row < height; row++)
            {   
                float randomFloat = (float)rand.NextDouble();
                island[row, col] = randomFloat <= spawnChance ? 60 : 10;
            }
        }
    }

    void buildIsland()
    {
        ref int width = ref roomModel.Width();
        ref int height = ref roomModel.Height();        
        ref int[,] island = ref roomModel.Island();
        ref int oceanCutoff = ref roomModel.OceanCutoff();
        ref int grassCutoff = ref roomModel.GrassCutoff();
        ref Vector3Int pos = ref roomModel.Pos();
        ref Vector3Int startPos = ref roomModel.StartPos();
        ref RuleTile ocean = ref roomModel.Ocean();
        ref RuleTile grass = ref roomModel.Grass();
        ref Tile sand = ref roomModel.Sand();
        ref Tilemap map = ref roomView.Map();
        for(int row=height-1; row>-1; row--)
        {
            for(int col=0; col < width; col++)
            {
                pos = new Vector3Int(col + startPos.x, startPos.y + row, 0);
                if(island[row, col] > grassCutoff){
                    map.SetTile(pos, grass);
                }else if(island[row, col] > oceanCutoff){
                    map.SetTile(pos, sand);
                }else{
                    map.SetTile(pos, ocean);
                }
            }
        }
    }

    void generateIslandMap()
    {
        ref int width = ref roomModel.Width();
        ref int height = ref roomModel.Height();
        ref int[,] island = ref roomModel.Island();
        ref int[,] newIsland = ref roomModel.NewIsland();
        ref System.Random rand = ref roomModel.Rand();

        for(int row=1; row < height-1; row++)
        {
            for(int col=1; col<width-1; col++)
            {
                int neighbours = 0;
                int self = island[row, col];

                for(int i= -1; i<=1; i++)
                {
                    for(int j=-1; j<=1; j++)
                    {
                        neighbours = neighbours + island[row+i, col+j];
                    }
                }
                
                neighbours = neighbours - self;
                neighbours = neighbours / 8;

                if(neighbours < 40)
                {
                    if(neighbours - self < 0){
                        newIsland[row, col] = self + rand.Next(neighbours - self, 0);
                    }else{
                        newIsland[row, col] = self + rand.Next(neighbours - self);
                    }
                }else if(self > neighbours){
                    newIsland[row, col] = self + rand.Next(-10, 10);
                }else{
                    newIsland[row, col] = self + rand.Next(neighbours/5);
                }
            }
        }
        island = newIsland;
    }

    void cleanupOcean()
    {
        ref int width = ref roomModel.Width();
        ref int height = ref roomModel.Height();
        ref int[,] island = ref roomModel.Island();
        ref int oceanCutoff = ref roomModel.OceanCutoff();
        ref int grassCutoff = ref roomModel.GrassCutoff();
        ref Tilemap map = ref roomView.Map();
        ref RuleTile ocean = ref roomModel.Ocean();
        ref Tile sand = ref roomModel.Sand();
        ref Vector3Int startPos = ref roomModel.StartPos();

        for(int row=1; row < height-1; row++)
        {
            for(int col=1; col<width-1; col++)
            {
                int cur = island[row, col];                
                if(cur <= oceanCutoff)
                {
                    //Ocean cleanup
                    //We dont want ocean blocks in the middle of the landmass(es) or dips in our coastline
                    int neighbours = 0;
                    for(int i= -1; i<=1; i++)
                    {
                        for(int j=-1; j<=1; j++)
                        {
                            if(island[row+i, col+j] > 95)
                            {
                                neighbours += 1;
                            }
                        }
                    }
                    if(neighbours >= 5)
                    {                 
                        island[row, col] = 110;          
                        Vector3Int pos = new Vector3Int(col + startPos.x, startPos.y + row, 0);
                        map.SetTile(pos, sand);
                    }
                }else if(cur > oceanCutoff && cur <=grassCutoff){
                    //Sand Cleanup
                    //We dont want any random sand blocks in the middle of the ocean
                    //Or any rough edges sticking out of our island(s)
                    int neighbours = 0;
                    for(int i= -1; i<=1; i++)
                    {
                        for(int j=-1; j<=1; j++)
                        {
                            if(island[row+i, col+j] <= 95)
                            {
                                neighbours += 1;
                            }
                        }
                    }
                    if(neighbours >= 5)
                    {            
                        island[row, col] = 80;                
                        Vector3Int pos = new Vector3Int(col + startPos.x, startPos.y + row, 0);
                        map.SetTile(pos, ocean);
                    }
                }else if(cur > grassCutoff){
                    //Grass Cleanup
                    //Due to rule tiles, we dont want and ocean blocks adjacent to grass
                    if(island[row-1, col] <=95 || island[row+1, col] <=95 || island[row, col-1] <=95 || island[row, col+1] <=95)
                    {
                        island[row, col] = 110;                
                        Vector3Int pos = new Vector3Int(col + startPos.x, startPos.y + row, 0);
                        map.SetTile(pos, sand);
                    }
                }
            }
        }
    } 

    void cleanupGrass()
    {
        ref int width = ref roomModel.Width();
        ref int height = ref roomModel.Height();
        ref int[,] island = ref roomModel.Island();
        ref int oceanCutoff = ref roomModel.OceanCutoff();
        ref int grassCutoff = ref roomModel.GrassCutoff();
        ref Tilemap map = ref roomView.Map();
        ref RuleTile grass = ref roomModel.Grass();
        ref Tile sand = ref roomModel.Sand();
        ref Vector3Int startPos = ref roomModel.StartPos();

        for(int row=1; row < height-1; row++)
        {
            for(int col=1; col<width-1; col++)
            {
                int cur = island[row, col];
                if(cur > oceanCutoff && cur <=grassCutoff)
                {
                    //Sand cleanup
                    //We dont want random bits of sand in our grass patches
                    int neighbours = 0;
                    for(int i= -1; i<=1; i++)
                    {
                        for(int j=-1; j<=1; j++)
                        {
                            if(island[row+i, col+j] > 135)
                            {
                                neighbours += 1;
                            }
                        }
                    }
                    if(neighbours >= 5)
                    {            
                        island[row, col] = 150;                
                        Vector3Int pos = new Vector3Int(col + startPos.x, startPos.y + row, 0);
                        map.SetTile(pos, grass);
                    }
                }else if(cur > grassCutoff){
                    //Grass cleanup
                    //We dont want random bits of grass in our sand
                    int neighbours = 0;
                    for(int i= -1; i<=1; i++)
                    {
                        for(int j=-1; j<=1; j++)
                        {
                            if(island[row+i, col+j] <= 135)
                            {
                                neighbours += 1;
                            }
                        }
                    }
                    if(neighbours >= 5)
                    {            
                        island[row, col] = 110;                
                        Vector3Int pos = new Vector3Int(col + startPos.x, startPos.y + row, 0);
                        map.SetTile(pos, sand);
                    }
                }
            }
        }
    }

    void generateGrassDetails()
    {
        ref int width = ref roomModel.Width();
        ref int height = ref roomModel.Height();
        ref int[,] island = ref roomModel.Island();
        ref int grassCutoff = ref roomModel.GrassCutoff();
        ref Tilemap details = ref roomView.Details();
        ref Tilemap map = ref roomView.Map();
        ref RuleTile grass = ref roomModel.Grass();
        ref Tile sand = ref roomModel.Sand();
        ref Vector3Int startPos = ref roomModel.StartPos();

        //Since we want to add grass borders, we will loop through the entire map and
        //Add all the grass pieces to the details tilemap(above floor) and then replace it with sand
        for(int row=height-1; row>-1; row--)
        {
            for(int col=0; col < width; col++)
            {   
                int cur = island[row, col];
                if(cur > grassCutoff){
                    Vector3Int pos = new Vector3Int(col + startPos.x, startPos.y + row, 0);
                    map.SetTile(pos, sand);
                    details.SetTile(pos, grass);
                }
            }
        }
    }

    void generateBorder()
    {
        ref int width = ref roomModel.Width();
        ref int height = ref roomModel.Height();
        ref Tilemap collision = ref roomView.Collision(); 
        ref Tile sand = ref roomModel.Sand();       
        ref Tile deepOcean = ref roomModel.DeepOcean();
        ref Vector3Int startPos = ref roomModel.StartPos();

        for(int row=-10; row < height + 10; row++)
        {
            for(int col=-10; col < width + 10; col++)
            {
                if((col<=1 || col>=width-1) || (row<=1 || row>=height-1))
                {
                    Vector3Int pos = new Vector3Int(col + startPos.x, startPos.y + row, 0);
                    collision.SetTile(pos, deepOcean);
                }
            }
        }       
        
    }

    public void AddLeftDock(int col, int row){
        ref int height = ref roomModel.Height();
        ref int width = ref roomModel.Width();
        ref GameObject dock = ref roomModel.Dock();
        ref Vector3Int startPos = ref roomModel.StartPos();
        ref Tilemap collision = ref roomView.Collision();

        Vector3Int dockPosition = new Vector3Int(startPos.x+1, startPos.y + (height/2), 0);
        Vector3 dockWorldPosition = collision.GetCellCenterWorld(dockPosition);
        GameObject newDock = Instantiate(dock, dockWorldPosition, Quaternion.identity);

        newDock.GetComponent<DockController>().AssignDirection("left");        
        newDock.GetComponent<DockController>().AssignRoom(row.ToString() + col.ToString());
    }

    public void AddRightDock(int col, int row){
        ref int height = ref roomModel.Height();
        ref int width = ref roomModel.Width();
        ref GameObject dock = ref roomModel.Dock();
        ref Vector3Int startPos = ref roomModel.StartPos();
        ref Tilemap collision = ref roomView.Collision();

        Vector3Int dockPosition = new Vector3Int(startPos.x + width-1, startPos.y + (height/2), 0);
        Vector3 dockWorldPosition = collision.GetCellCenterWorld(dockPosition);
        GameObject newDock = Instantiate(dock, dockWorldPosition, Quaternion.identity);
        
        newDock.GetComponent<DockController>().AssignDirection("right");
        newDock.GetComponent<DockController>().AssignRoom(row.ToString() + col.ToString());
    }

    public void AddUpDock(int col, int row){
        ref int height = ref roomModel.Height();
        ref int width = ref roomModel.Width();        
        ref GameObject dock = ref roomModel.Dock();
        ref Vector3Int startPos = ref roomModel.StartPos();
        ref Tilemap collision = ref roomView.Collision();

        Vector3Int dockPosition = new Vector3Int(startPos.x + (width/2), startPos.y+height-1, 0);
        Vector3 dockWorldPosition = collision.GetCellCenterWorld(dockPosition);
        GameObject newDock = Instantiate(dock, dockWorldPosition, Quaternion.identity);
        
        newDock.GetComponent<DockController>().AssignDirection("up");
        newDock.GetComponent<DockController>().AssignRoom(row.ToString() + col.ToString());
    }

    public void AddDownDock(int col, int row){
        ref int height = ref roomModel.Height();
        ref int width = ref roomModel.Width();
        ref GameObject dock = ref roomModel.Dock();
        ref Vector3Int startPos = ref roomModel.StartPos();
        ref Tilemap collision = ref roomView.Collision();

        Vector3Int dockPosition = new Vector3Int(startPos.x + (width/2), startPos.y+1, 0);
        Vector3 dockWorldPosition = collision.GetCellCenterWorld(dockPosition);
        GameObject newDock = Instantiate(dock, dockWorldPosition, Quaternion.identity);
        
        newDock.GetComponent<DockController>().AssignDirection("down");
        newDock.GetComponent<DockController>().AssignRoom(row.ToString() + col.ToString());
    }
}
