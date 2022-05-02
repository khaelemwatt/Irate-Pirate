using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatController : MonoBehaviour
{
    PlayerController playerController;
    MeatModel meatModel;

    void Start(){
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        meatModel = gameObject.GetComponent<MeatModel>();
    }
    
    public void ApplyStatEffect(){
        ref float statValue = ref meatModel.StatValue();
        playerController.RestoreHealth(statValue);
        Debug.Log("Increase");
    }
}
