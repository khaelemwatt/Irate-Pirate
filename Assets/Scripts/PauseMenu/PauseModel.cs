using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseModel : MonoBehaviour
{
    public bool isPaused;

    public GameObject menu;

    public ref bool IsPaused(){
        return ref this.isPaused;
    }

    public ref GameObject Menu(){
        return ref this.menu;
    }
}
