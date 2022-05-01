using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletModel : MonoBehaviour
{
    public int damage;

    public ref int Damage(){
        return ref this.damage;
    }
}
