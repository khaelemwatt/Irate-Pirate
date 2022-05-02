using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomModel : MonoBehaviour
{
    public float statValue;

    public ref float StatValue(){
        return ref this.statValue;
    }
}
