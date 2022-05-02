using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotController : MonoBehaviour
{
    ParrotModel parrotModel;

    void Start(){
        parrotModel = gameObject.GetComponent<ParrotModel>();
    }

    public void Die(){
        Destroy(gameObject);        
    }

    public void Attack(PlayerController playerController, Vector3 direction, Vector3 spawn){
        ref GameObject bullet = ref parrotModel.Bullet();
        ref float bulletForce = ref parrotModel.BulletForce();

        GameObject newBullet = Instantiate(bullet, spawn, Quaternion.identity);
        
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
        Destroy(newBullet, 2f);
    }
}
