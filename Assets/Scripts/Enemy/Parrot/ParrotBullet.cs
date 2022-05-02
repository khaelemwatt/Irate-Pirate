using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotBullet : MonoBehaviour
{
    PlayerController playerController;

    void OnTriggerEnter2D(Collision2D other){
        Debug.Log("bullet hit player");
        GameObject player = other.gameObject;
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if(player.CompareTag("PlayerHitbox")){
            playerController.Damage(5f);
            Destroy(gameObject);
        }
    }
}
