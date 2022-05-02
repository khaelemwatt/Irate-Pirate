using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinController : MonoBehaviour
{
    WinModel winModel;

    void Start(){
        winModel = gameObject.GetComponent<WinModel>();
    }

    public void Win(){
        ref GameObject win = ref winModel.Win();
        win.SetActive(true);
        Time.timeScale = 0f;
    }
}
 