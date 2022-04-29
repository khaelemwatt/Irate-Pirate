using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkeletonAttack : Enemy
{
    public override IEnumerator attack()
    {
        isAttacking = true;
        Debug.Log("Skeleton Attack");
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }
}