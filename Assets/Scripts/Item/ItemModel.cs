using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemModel : MonoBehaviour
{
    public string item;
    public string itemDesc;

    public GameObject itemPickupImage;
    public GameObject itemPickupText;
    public GameObject itemPickupDesc;

    public ref string Item(){
        return ref this.item;
    }

    public ref string ItemDesc(){
        return ref this.itemDesc;
    }

    public ref GameObject ItemPickupImage(){
        return ref this.itemPickupImage;
    }

    public ref GameObject ItemPickupText(){
        return ref this.itemPickupText;
    }

    public ref GameObject ItemPickupDesc(){
        return ref this.itemPickupDesc;
    }
}
