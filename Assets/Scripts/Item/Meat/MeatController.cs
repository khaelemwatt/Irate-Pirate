using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatController : MonoBehaviour
{
    PlayerModel playerModel;
    MeatModel meatModel;

    void Start(){
        playerModel = GameObject.FindWithTag("Player").GetComponent<PlayerModel>();
        meatModel = gameObject.GetComponent<MeatModel>();
    }
    
    public void ApplyStatEffect(){
        ref float statValue = ref meatModel.StatValue();
        //Stat Effect Here
    }
}
