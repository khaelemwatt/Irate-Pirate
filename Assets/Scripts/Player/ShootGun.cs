using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    //#--------------------# VARIABLES #--------------------#
    //#----------# Float #----------#
    public float bulletForce = 3f;

    //#----------# UNITY OBJECT #----------#
    public Transform firePoint;
    public GameObject bullet;  

     //#--------------------# SHOOT #--------------------#
    public void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Destroy(newBullet, 2f);
    }
}
