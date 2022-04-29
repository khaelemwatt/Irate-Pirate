using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MageAttack : Enemy
{
    public float timer;

    public override IEnumerator attack()
    {
        timer = 0;
        isAttacking = true;
        yield return new WaitForSeconds(1f);
        isAttacking = false;

        Vector2 playerPos = player.transform.position;
        Vector2 currentPos = transform.position;
        Vector2 fleeDir = currentPos - playerPos;
        fleeDir = fleeDir.normalized;
        Vector2 fleePosition = new Vector2(transform.position.x + fleeDir.x, transform.position.y + fleeDir.y);

        while(timer < 1)
        {
            Debug.Log("Fleeing");
            timer += Time.fixedDeltaTime;
            rb.MovePosition(rb.position + fleeDir * movementSpeed * Time.fixedDeltaTime);
        }
        Debug.Log("Mage Attacking");


        //Flee
        //Attack
    }
}
