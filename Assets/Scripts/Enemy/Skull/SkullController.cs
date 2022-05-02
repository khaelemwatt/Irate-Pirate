using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullController : MonoBehaviour
{
    SkullModel skullModel;

    void Start(){
        skullModel = gameObject.GetComponent<SkullModel>();
    }

    public void Die(){
        Destroy(gameObject);        
    }

    public void Attack(PlayerController playerController){
        ref float damage = ref skullModel.Damage();
        playerController.Damage(damage);
    }
}
