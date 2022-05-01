using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusketController : MonoBehaviour
{
    MusketModel musketModel;

    void Start(){
        musketModel = gameObject.GetComponent<MusketModel>();
    }

    //#--------------------# SHOOT #--------------------#
    public void Shoot()
    {
        ref GameObject bullet = ref musketModel.Bullet();
        ref Transform firePoint = ref musketModel.FirePoint();
        ref float bulletForce = ref musketModel.BulletForce();
        ref AudioSource audioSource = ref musketModel.AudioSource();
        ref AudioClip shot = ref musketModel.Shot();

        GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        audioSource.PlayOneShot(shot);
        Destroy(newBullet, 1f);
    }
}
