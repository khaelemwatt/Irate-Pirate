using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    InventoryModel inventoryModel;

    void Start(){
        inventoryModel = gameObject.GetComponent<InventoryModel>();

        ref int currentInvSlot = ref inventoryModel.CurrentInvSlot();
        ref Dictionary<string, GameObject> allWeapons = ref inventoryModel.AllWeapons();
        ref List<GameObject> weapons = ref inventoryModel.Weapons();
        ref GameObject musket = ref inventoryModel.Musket();
        ref GameObject blunderbuss = ref inventoryModel.Blunderbuss(); 
        ref GameObject startWeapon = ref inventoryModel.StartWeapon();
        ref GameObject currentWeapon = ref inventoryModel.CurrentWeapon();

        //ID List for all weapons. Used to spawn weapons in
        allWeapons = new Dictionary<string, GameObject>()
        {
            {"Blunderbuss", blunderbuss},
            {"Musket", musket}
        };

        currentInvSlot = 0;
        currentWeapon = startWeapon;
        addWeapon(startWeapon.GetComponent<GunModel>().Gun());
        createWeapon(currentInvSlot);        
    }

    public void switchToWeapon(int invSlot)
    {   
        ref int currentInvSlot = ref inventoryModel.CurrentInvSlot();
        ref List<GameObject> weapons = ref inventoryModel.Weapons();
        ref GameObject currentWeapon = ref inventoryModel.CurrentWeapon();
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
        ref List<GameObject> weapons = ref inventoryModel.Weapons();
        ref GameObject currentWeapon = ref inventoryModel.CurrentWeapon();
        ref GameObject weaponHolder = ref inventoryModel.WeaponHolder();

        currentWeapon = Instantiate(weapons[invSlot], weaponHolder.transform.position, Quaternion.identity);
        currentWeapon.transform.parent = weaponHolder.transform;
    }

    public void addWeapon(string gun)
    {
        ref List<GameObject> weapons = ref inventoryModel.Weapons();
        ref Dictionary<string, GameObject> allWeapons = ref inventoryModel.AllWeapons();
        ref List<GameObject> invSlots = ref inventoryModel.InvSlots();

        //string gunName = gun.GetComponent<GunModel>().Gun();

        int place = weapons.Count;
        GameObject weaponToAdd = allWeapons[gun];
        weapons.Add(weaponToAdd);
        ref Sprite gunSprite = ref weaponToAdd.GetComponent<GunModel>().GunSprite();
        invSlots[place].GetComponent<Image>().sprite = gunSprite;

        // Vector3 invPosition = invSlots[place].transform.position;
        // Vector3 invScale = invSlots[place].transform.localScale;
        // invPosition = new Vector3(invPosition.x, invPosition.y - 0.056f, invPosition.z);
        // invScale = new Vector3(2.57f, 2.57f, invScale.z);
    }
}
