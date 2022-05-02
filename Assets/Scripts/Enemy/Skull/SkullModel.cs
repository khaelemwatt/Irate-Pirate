using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullModel : MonoBehaviour
{
    public float damage;

    public ref float Damage(){
        return ref this.damage;
    }
}
