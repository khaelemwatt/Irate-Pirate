using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
    PlayerController playerController;

    void OnTriggerEnter2D(Collider2D other){
        GameObject bullet = other.gameObject;
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if(bullet.CompareTag("EnemyBullet")){
            playerController.Damage(5f);
            Destroy(bullet);
        }
    }
}
