using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlunderbussModel : MonoBehaviour
{
    //#--------------------# VARIABLES #--------------------#
    //#----------# Float #----------#
    public float bulletForce = 3f;

    //#----------# UNITY OBJECT #----------#
    public Transform firePoint;
    public GameObject bullet; 

    public ref float BulletForce(){
        return ref this.bulletForce;
    }

    public ref Transform FirePoint(){
        return ref this.firePoint;
    }

    public ref GameObject Bullet(){
        return ref this.bullet;
    }
}
