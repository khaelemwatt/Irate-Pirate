using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{   
    //#--------------------# ONTRIGGERENTER2D #--------------------#
    public void OnTriggerEnter2D(Collider2D other)
    {
        string gunName = other.gameObject.GetComponent<GunCollision>().getGun();
        Sprite gunSprite = other.gameObject.GetComponent<GunCollision>().getSprite();
        gameObject.GetComponent<PlayerShoot>().addWeapon(gunName, gunSprite);
        Destroy(other.gameObject.transform.parent.gameObject);
    }
}
