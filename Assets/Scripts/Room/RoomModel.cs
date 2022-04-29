using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class RoomModel : MonoBehaviour
{
    public int width = 100;
    public int height = 100;
    public int oceanCutoff;
    public int grassCutoff;
    
    public float spawnChance = 0.4f;
    
    public Vector3Int pos = new Vector3Int(0, 0, 0);
    public Vector3Int startPos;

    public System.Random rand = new System.Random();

    public GameObject dock;
    
    public Tile deepOcean;
    public RuleTile ocean;    
    public RuleTile grass;
    public Tile sand;

    public int[,] island;
    public int[,] newIsland;

    public ref int Width(){
        return ref this.width;
    }

    public ref int Height(){
        return ref this.height;
    }

    public ref int OceanCutoff(){
        return ref this.oceanCutoff;
    }

    public ref int GrassCutoff(){
        return ref this.grassCutoff;
    }

    public ref float SpawnChance(){
        return ref this.spawnChance;
    }

    public ref Vector3Int Pos(){
        return ref this.pos;
    }

    public ref Vector3Int StartPos(){
        return ref this.startPos;
    }

    public ref System.Random Rand(){
        return ref this.rand;
    }

    public ref GameObject Dock()
    {
        return ref this.dock;
    }

    public ref Tile DeepOcean(){
        return ref this.deepOcean;
    }

    public ref RuleTile Ocean(){
        return ref this.ocean;
    }

    public ref RuleTile Grass(){
        return ref this.grass;
    }

    public ref Tile Sand(){
        return ref this.sand;
    }

    public ref int[,] Island(){
        return ref this.island;
    }

    public ref int[,] NewIsland(){
        return ref this.newIsland;
    }
}
