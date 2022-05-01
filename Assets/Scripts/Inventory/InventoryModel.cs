using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel : MonoBehaviour
{    
    public int currentInvSlot;

    public GameObject weaponHolder;
    public GameObject currentWeapon;
    public GameObject blunderbuss;
    public GameObject musket;
    public GameObject startWeapon;
    //public GameObject bullet;

    public Dictionary<string, GameObject> allWeapons;

    public List<GameObject> weapons = new List<GameObject>();
    public List<GameObject> invSlots = new List<GameObject>();

    public ref int CurrentInvSlot(){
        return ref this.currentInvSlot;
    }

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

    public ref GameObject StartWeapon(){
        return ref this.startWeapon;
    }

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
