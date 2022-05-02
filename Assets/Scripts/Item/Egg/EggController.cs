using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    PlayerModel playerModel;
    PlayerController playerController;
    EggModel eggModel;

    void Start(){
        playerModel = GameObject.FindWithTag("Player").GetComponent<PlayerModel>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        eggModel = gameObject.GetComponent<EggModel>();
    }
    
    public void ApplyStatEffect(){
        ref float statValue = ref eggModel.StatValue();
        ref float maxHealth = ref playerModel.MaxHealth();
        maxHealth += statValue;
        playerController.RestoreHealth(statValue);
        Debug.Log("Increase");
    }
}
