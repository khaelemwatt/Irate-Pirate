using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Health bar made with help from https://www.youtube.com/watch?v=BLfNP4Sc_iA

public class HealthbarController : MonoBehaviour
{
    HealthbarModel healthbarModel;

    public void SetMaxHealth(int health){
        healthbarModel = gameObject.GetComponent<HealthbarModel>();
        ref Slider slider = ref healthbarModel.Slider();
        slider.maxValue = health;
    }

    public void SetHealth(int health){
        healthbarModel = gameObject.GetComponent<HealthbarModel>();
        ref Slider slider = ref healthbarModel.Slider();
        slider.value = health;
    }
}
