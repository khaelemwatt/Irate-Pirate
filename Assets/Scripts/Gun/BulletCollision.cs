using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//#------------------------------# CREDIT #------------------------------#

//From Video (bullet system)
//https://www.youtube.com/watch?v=LNLVOjbrQj4&list=PL8z36fSARS2MmrHH67XoYnI6OJdB8oPar&index=7




public class BulletCollision : MonoBehaviour, IDamager

{
    //#--------------------# ONCOLLISIONENTER2D #--------------------#
    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject enemy = collision.collider.gameObject;
        //Debug.Log("10");
        if(enemy.CompareTag("Enemy"))
        {
            enemy.GetComponent<EnemyController>().Damage(5);
        }
        
        Destroy(gameObject);
    }
}
