using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusketModel : MonoBehaviour
{
    //#--------------------# VARIABLES #--------------------#
    //#----------# Float #----------#
    public float bulletForce = 4f;
    public float reloadTime;

    //#----------# UNITY OBJECT #----------#
    public Transform firePoint;
    public GameObject bullet; 
    public AudioSource audioSource;
    public AudioClip shot;    

    public ref float BulletForce(){
        return ref this.bulletForce;
    }

    public ref float ReloadTime(){
        return ref this.reloadTime;
    }

    public ref Transform FirePoint(){
        return ref this.firePoint;
    }

    public ref GameObject Bullet(){
        return ref this.bullet;
    }

    public ref AudioSource AudioSource(){
        return ref this.audioSource;
    }

    public ref AudioClip Shot(){
        return ref this.shot;
    }
}
