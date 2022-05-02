using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapModel : MonoBehaviour
{
    //#----------# Float #----------#

    //#----------# Int #----------#
    public int width;
    public int height;    
    public int randomVal;
    public int[,] level;

    //#----------# Bool #----------# 

    //#----------# Unity Object #----------#

    //#----------# List #----------#
    public List<Tuple<string, bool>> rooms;
    public List<GameObject> enemy;

    //#----------# GameObject #----------#    
    public GameObject[] enemies;
    public GameObject player;
    public GameObject bossEnemy;
    public GameObject[] docks;

    //#----------# System #----------#    
    System.Random rand = new System.Random();

    public AudioClip backMusic;
    public AudioClip bossMusic;

    public AudioSource audioSource;


    //#--------------------# REFERENCE METHODS #--------------------#
    //#----------# Int #----------#
    public ref int Width(){
        return ref this.width;
    }
    public ref int Height(){
        return ref this.height;
    }
    public ref int RandomVal(){
        return ref this.randomVal;
    }
    public ref int[,] Level(){
        return ref this.level;
    }
    
    public ref List<Tuple<string, bool>> Rooms(){
        return ref this.rooms;
    }

    public ref GameObject[] Enemies(){
        return ref this.enemies;
    }

    public ref GameObject Player(){
        return ref this.player;
    }

    public ref List<GameObject> Enemy(){
        return ref this.enemy;
    }

    public ref GameObject BossEnemy(){
        return ref this.bossEnemy;
    }

    public ref GameObject[] Docks(){
        return ref this.docks;
    }

    public ref System.Random Rand(){
        return ref this.rand;
    }

    public ref AudioClip BackMusic(){
        return ref this.backMusic;
    }

    public ref AudioClip BossMusic(){
        return ref this.bossMusic;
    }

    public ref AudioSource AudioSource(){
        return ref this.audioSource;
    }
}
