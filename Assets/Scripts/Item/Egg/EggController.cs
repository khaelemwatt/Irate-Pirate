using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    PlayerModel playerModel;
    EggModel eggModel;

    void Start(){
        playerModel = GameObject.FindWithTag("Player").GetComponent<PlayerModel>();
        eggModel = gameObject.GetComponent<EggModel>();
    }
    
    public void ApplyStatEffect(){
        ref float statValue = ref eggModel.StatValue();
        //Stat Effect Here
        Debug.Log("Increase");
    }
}
