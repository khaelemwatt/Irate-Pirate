using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinModel : MonoBehaviour
{
    public GameObject win;

    public ref GameObject Win(){
        return ref this.win;
    }
}
