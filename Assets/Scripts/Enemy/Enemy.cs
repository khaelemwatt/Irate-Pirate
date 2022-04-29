using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour//, IDamageable
{
    public GameObject player;
    public Rigidbody2D rb;
    public float movementSpeed = 1f;
    public float lineOfSight = 2f;
    public float distanceToPlayer;
    public float wanderCounter = 0f;
    public float idle;
    public bool isAttacking = false;
    public bool isWandering = false;
    public Vector2 wanderTarget;
    public Vector2 playerPos;
    public Vector2 enemyPos;
    public Vector2 wanderDir;

    System.Random rand = new System.Random();

    public int health;

    public void Start()
    {
        health = 10;
    }
    
    public void Damage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        playerPos = player.transform.position;
        enemyPos = transform.position;
        distanceToPlayer = (enemyPos - playerPos).magnitude;
        if(distanceToPlayer <= lineOfSight)
        {
            if(isAttacking == false)
            {
                StartCoroutine(attack());
            }            
            Vector2 dirToPlayer = playerPos - rb.position;
            moveTo(dirToPlayer);
        }else{
            if(isWandering==false){
                StartCoroutine(wander()); 
            }                       
        }
    }

    IEnumerator wander()
    {
        isWandering = true;
        wanderCounter = (float)rand.Next(2);
        wanderTarget = new Vector2(rb.position.x + randomFloat(), rb.position.y + randomFloat());
        wanderDir = wanderTarget - rb.position;
        //As long as player isnt in our line of sight we continue to wander
        
        while(wanderCounter > 0)
        {            
            moveTo(wanderDir);
            yield return new WaitForSeconds(0.01f);
            wanderCounter -= Time.deltaTime;
        }
        yield return new WaitForSeconds(1f);
        isWandering= false;
    }

    float randomFloat()
    {
        float value = (float)rand.NextDouble();
        if(value >= 0.5){
            value = value/2f;
        }
        float newValue =  rand.NextDouble() <= 0.5 ? value*(-1) : value;
        return newValue;
    }

    void moveTo(Vector2 dir)
    {
        rb.MovePosition(rb.position + dir * movementSpeed * Time.fixedDeltaTime);
    }

    public virtual IEnumerator attack()
    {
        isAttacking = true;
        Debug.Log("Attack");
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }
}
