using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarModel : MonoBehaviour
{
    public Slider slider;

    public ref Slider Slider(){
        return ref this.slider;
    }
}
