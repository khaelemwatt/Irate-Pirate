using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gun rotation from video
//https://www.youtube.com/watch?v=dGy0_UKjqSs

public class GunController : MonoBehaviour
{
    GunModel gunModel;

    void Start(){
        gunModel = gameObject.GetComponent<GunModel>();
    }

    //#--------------------# UPDATE #--------------------#
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos =  Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        
        transform.right = direction;        
    }

    public void ShootGun(){
        ref string gun = ref gunModel.Gun();
        switch(gun){
            case "Blunderbuss":
                BlunderbussController blunderbussController = gameObject.GetComponent<BlunderbussController>();
                blunderbussController.Shoot();
                break;
            case "Musket":
                // MusketController musketController = gameObject.GetComponent<MusketController>();
                // musketController.Shoot();
                break;
        }
    }    
}
