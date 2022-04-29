using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemModel : MonoBehaviour
{
    public string item;

    public ref string Item(){
        return ref this.item;
    }
}
