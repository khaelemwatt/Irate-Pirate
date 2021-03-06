using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlunderbussController : MonoBehaviour
{
    BlunderbussModel blunderbussModel;

    void Start(){
        blunderbussModel = gameObject.GetComponent<BlunderbussModel>();
    }

    //#--------------------# SHOOT #--------------------#
    public void Shoot()
    {
        ref GameObject bullet = ref blunderbussModel.Bullet();
        ref Transform firePoint = ref blunderbussModel.FirePoint();
        ref float bulletForce = ref blunderbussModel.BulletForce();
        ref AudioSource audioSource = ref blunderbussModel.AudioSource();
        ref AudioClip shot = ref blunderbussModel.Shot();

        GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        audioSource.PlayOneShot(shot);
        Destroy(newBullet, 1f);
    }
}
