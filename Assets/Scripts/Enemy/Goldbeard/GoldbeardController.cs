using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldbeardController : MonoBehaviour
{
    GoldbeardModel goldbeardModel;
    WinController winController;

    void Start(){
        goldbeardModel = gameObject.GetComponent<GoldbeardModel>();
        winController  = GameObject.FindWithTag("UI").GetComponent<WinController>();
    }

    public void Die(){
        winController.Win();
        Destroy(gameObject);
    }

    public void Attack(PlayerController playerController){
        //show animation
        ref Animator animator = ref goldbeardModel.Animator();
        ref float damage = ref goldbeardModel.Damage();
        animator.SetBool("attacking", true);
        StartCoroutine(TurnOffAttackAnim());
        //damage player
        playerController.Damage(damage);
    }

    IEnumerator TurnOffAttackAnim(){        
        yield return new WaitForSeconds(0.1f);
        goldbeardModel.animator.SetBool("attacking", false);
    }
}
