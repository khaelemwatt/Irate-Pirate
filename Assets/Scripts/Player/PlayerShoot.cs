using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//#------------------------------# CREDIT #------------------------------#

//From video (bullet system)
//https://www.youtube.com/watch?v=LNLVOjbrQj4&list=PL8z36fSARS2MmrHH67XoYnI6OJdB8oPar&index=7




public class PlayerShoot : MonoBehaviour
{   
    //#--------------------# VARIABLES #--------------------#
    //#----------# Weapons/Inventory #----------#
    public GameObject weaponHolder;
    public int currentInvSlot;
    public GameObject currentWeapon;
    public GameObject blunderbuss;
    public GameObject musket;
    public Dictionary<string, GameObject> allWeapons;
    public List<GameObject> weapons = new List<GameObject>();
    public List<GameObject> invSlots = new List<GameObject>();

    //#--------------------# START #--------------------#
    void Start()
    {      
        currentInvSlot = 0;
        weapons.RemoveAt(0);
        weapons.Add(blunderbuss);
        
        createWeapon(currentInvSlot);

        //ID List for all weapons. Used to spawn weapons in
        allWeapons = new Dictionary<string, GameObject>()
        {
            {"blunderbuss", blunderbuss},
            {"musket", musket}
        };
    }

    //#--------------------# UPDATE #--------------------#
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {            
            currentWeapon.GetComponent<ShootGun>().Shoot();
        }

        if(Input.GetKeyDown("1"))
            switchToWeapon(0);

        if(Input.GetKeyDown("2"))
            switchToWeapon(1);
            
        if(Input.GetKeyDown("3"))
            switchToWeapon(2);
    }

    //#--------------------# SWITCHTOWEAPON #--------------------#
    void switchToWeapon(int invSlot)
    {   
        if(currentInvSlot != invSlot && weapons.Count >= invSlot + 1)
        {
            if(weapons[invSlot] != null)
            {
                Destroy(currentWeapon);
                createWeapon(invSlot);                
                currentInvSlot = invSlot;
            }            
        }        
    }

    //#--------------------# CREATEWEAPON #--------------------#
    public void createWeapon(int invSlot){
        currentWeapon = Instantiate(weapons[invSlot], transform.position, Quaternion.identity);
        currentWeapon.transform.parent = weaponHolder.transform;
    }

    //#--------------------# ADDWEAPON #--------------------#
    public void addWeapon(string gunName, Sprite gunSprite)
    {
        int place = weapons.Count;
        weapons.Add(allWeapons[gunName]);
        // invSlots[place].GetComponent<SpriteRenderer>().sprite = gunSprite;

        // Vector3 invPosition = invSlots[place].transform.position;
        // Vector3 invScale = invSlots[place].transform.localScale;
        // invPosition = new Vector3(invPosition.x, invPosition.y - 0.056f, invPosition.z);
        // invScale = new Vector3(2.57f, 2.57f, invScale.z);
    }
}
