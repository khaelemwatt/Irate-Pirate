using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    PlayerModel playerModel;
    MushroomModel mushroomModel;

    void Start(){
        playerModel = GameObject.FindWithTag("Player").GetComponent<PlayerModel>();
        mushroomModel = gameObject.GetComponent<MushroomModel>();
    }
    
    public void ApplyStatEffect(){
        ref float statValue = ref mushroomModel.StatValue();
        ref float movementSpeed = ref playerModel.MovementSpeed();
        movementSpeed += statValue;
        Debug.Log("Increase");
    }
}
