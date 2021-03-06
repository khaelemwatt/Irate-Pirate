using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{

    public string enemyName;

    //#----------# Float #----------#
    public float movementSpeed = 1f;
    public float lineOfSight = 2f;
    public float distanceToPlayer;
    public float wanderCounter = 0f;

    //#----------# Int #----------#
    public int health;

    //#----------# Bool #----------# 
    public bool isAttacking = false;
    public bool isWandering = false;
    public bool isInvulnerable = false;
    public bool isTouchingBorder = false;
    public bool isTouchingPlayer = false;

    //#----------# Unity Object #----------#
    public Rigidbody2D rb;
    public Animator animator;

    //#----------# Vector2 #----------#
    public Vector2 wanderTarget;
    public Vector2 playerPos;
    public Vector2 enemyPos;
    public Vector2 wanderDir;
    public Vector2 dirToPlayer;

    //#----------# GameObject #----------#
    public GameObject player;

    //#----------# System #----------#
    public System.Random rand = new System.Random();

    //#--------------------# REFERENCE METHODS #--------------------#

    public ref string EnemyName(){
        return ref this.enemyName;
    }

    //#----------# Float #----------#
    public ref float MovementSpeed(){
        return ref this.movementSpeed;
    }

    public ref float LineOfSight(){
        return ref this.lineOfSight;
    }

    public ref float DistanceToPlayer(){
        return ref this.distanceToPlayer;
    }

    public ref float WanderCounter(){
        return ref this.wanderCounter;
    }

    //#----------# Int #----------#
    public ref int Health(){
        return ref this.health;
    }

    //#----------# Bool #----------#
     public ref bool IsAttacking(){
        return ref this.isAttacking;
    }
    public ref bool IsWandering(){
        return ref this.isWandering;
    }

    public ref bool IsInvulnerable(){
        return ref this.isInvulnerable;
    }

    public ref bool IsTouchingBorder(){
        return ref this.isTouchingBorder;
    }

    public ref bool IsTouchingPlayer(){
        return ref this.isTouchingPlayer;
    }

    //#----------# Unity Object #----------#
    public ref Rigidbody2D Rb(){
        return ref this.rb;
    }

    public ref Animator Animator(){
        return ref this.animator;
    }

    //#----------# Vector2 #----------#
    public ref Vector2 WanderTarget(){
        return ref this.wanderTarget;
    }

    public ref Vector2 PlayerPos(){
        return ref this.playerPos;
    }
    public ref Vector2 EnemyPos(){
        return ref this.enemyPos;
    }

    public ref Vector2 WanderDir(){
        return ref this.wanderDir;
    }

    public ref Vector2 DirToPlayer(){
        return ref this.dirToPlayer;
    }

    //#----------# GameObject #----------#
    public ref GameObject Player(){
        return ref this.player;
    }

    //#----------# System #----------#
    public ref System.Random Rand(){
        return ref this.rand;
    }    
}


