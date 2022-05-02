using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldbeardModel : MonoBehaviour
{
    public float damage;

    public Animator animator;

    public ref float Damage(){
        return ref this.damage;
    }

    public ref Animator Animator(){
        return ref this.animator;
    }
}
