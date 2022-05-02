using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public List<GameObject> allItems;

    public ref List<GameObject> AllItems(){
        return ref this.allItems;
    }
}
