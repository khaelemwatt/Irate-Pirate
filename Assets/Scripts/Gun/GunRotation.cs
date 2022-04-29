using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//#------------------------------# CREDIT #------------------------------#

//Gun rotation from video
//https://www.youtube.com/watch?v=dGy0_UKjqSs




public class GunRotation : MonoBehaviour
{   
    //#--------------------# UPDATE #--------------------#
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos =  Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        
        transform.right = direction;        
    }
}
