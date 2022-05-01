using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupModel : MonoBehaviour
{
    public string gunName;

    public ref string GunName(){
        return ref this.gunName;
    }
}
