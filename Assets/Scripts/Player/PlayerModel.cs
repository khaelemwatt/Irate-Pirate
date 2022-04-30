using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    //#--------------------# VARIABLES #--------------------#
    //#----------# Float #----------#
    public float movementSpeed;
    public float rollSpeed;
    public float rollLength;
    public float rollCooldown;
    public float rollCounter;
    public float rollCoolCounter;
    public float bulletForce = 3f;
    public float health;

    //#----------# Int #----------#
    public int currentInvSlot;
    public string currentRoom;

    //#----------# Bool #----------#
    public bool isRolling;   
    public bool isInvulnerable;
    public bool isTouchingEnemy;

    //#----------# Unity Object #----------#
    public Rigidbody2D rb;
    public Camera cam;
    public Transform firePoint;
    public Animator playerAnimator;

    //#----------# Vector2 #----------#
    public Vector2 mouseDir;
    public Vector2 mousePos;
    public Vector2 movement;
    public Vector2 direction;

    //#----------# GameObject #----------#
    public GameObject weaponHolder;
    public GameObject currentWeapon;
    public GameObject blunderbuss;
    public GameObject musket;
    public GameObject bullet; 

    //#----------# List/Dictionary #----------#
    public Dictionary<string, GameObject> allWeapons;

    public List<GameObject> weapons = new List<GameObject>();
    public List<GameObject> invSlots = new List<GameObject>();

    
    //#--------------------# REFERENCE METHODS #--------------------#
    //#----------# Float #----------#
    public ref float MovementSpeed(){
        return ref this.movementSpeed;
    }

    public ref float RollSpeed(){
        return ref this.rollSpeed;
    }

    public ref float RollLength(){
        return ref this.rollLength;
    }

    public ref float RollCooldown(){
        return ref this.rollCooldown;
    }

    public ref float RollCounter(){
        return ref this.rollCounter;
    }

    public ref float RollCoolCounter(){
        return ref this.rollCoolCounter;
    }

    public ref float BulletForce(){
        return ref this.bulletForce;
    }

    public ref float Health(){
        return ref this.health;
    }

    //#----------# Int #----------#
    public ref int CurrentInvSlot(){
        return ref this.currentInvSlot;
    }

    public ref string CurrentRoom(){
        return ref this.currentRoom;
    }

    //#----------# Bool #----------#
    public ref bool IsRolling(){
        return ref this.isRolling;
    }

    public ref bool IsInvulnerable(){
        return ref this.isInvulnerable;
    }

    public ref bool IsTouchingEnemy(){
        return ref this.isTouchingEnemy;
    }

    //#----------# Unity Object #----------#
    public ref Rigidbody2D Rb(){
        return ref this.rb;
    }

    public ref Camera Cam(){
        return ref this.cam;
    }

    public ref Transform FirePoint(){
        return ref this.firePoint;
    }

    public ref Animator PlayerAnimator(){
        return ref this.playerAnimator;
    }

    //#----------# Vector2 #----------#
    public ref Vector2 MouseDir(){
        return ref this.mouseDir;
    }

    public ref Vector2 MousePos(){
        return ref this.mousePos;
    }

    public ref Vector2 Movement(){
        return ref this.movement;
    }

    public ref Vector2 Direction(){
        return ref this.direction;
    }

    //#----------# GameObject #----------#
    public ref GameObject WeaponHolder(){
        return ref this.weaponHolder;
    }

    public ref GameObject CurrentWeapon(){
        return ref this.currentWeapon;
    }

    public ref GameObject Blunderbuss(){
        return ref this.blunderbuss;
    }

    public ref GameObject Musket(){
        return ref this.musket;
    }

    public ref GameObject Bullet(){
        return ref this.bullet;
    }

    //#----------# List/Dictionary #----------#
    public ref Dictionary<string, GameObject> AllWeapons(){
        return ref this.allWeapons;
    }

    public ref List<GameObject> Weapons(){
        return ref this.weapons;
    }

    public ref List<GameObject> InvSlots(){
        return ref this.invSlots;
    }
}
