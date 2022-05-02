using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotModel : MonoBehaviour
{
    public float damage;
    public float bulletForce;

    public GameObject bullet;

    public ref float Damage(){
        return ref this.damage;
    }

    public ref float BulletForce(){
        return ref this.bulletForce;
    }

    public ref GameObject Bullet(){
        return ref this.bullet;
    }
}
