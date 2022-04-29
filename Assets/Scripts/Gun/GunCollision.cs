using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCollision : MonoBehaviour
{   //#--------------------# VARIABLES #--------------------#
    public string gunName;
    public Sprite gunSprite;
    public int rotationSpeed;
    public float bobSpeed;
    public float height;

    //#--------------------# UPDATE #--------------------#
    void Update()
    {
        //Rotate in circles
        transform.Rotate(Vector3.up * (Time.deltaTime * rotationSpeed));

        //Bob up and down
        float sinYValue = Mathf.Sin(Time.time * bobSpeed) * height;
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + sinYValue, 13);
    }

    //#--------------------# GETGUN #--------------------#
    public string getGun()
    {
        return gunName;
    }

    //#--------------------# GETSPRITE #--------------------#
    public Sprite getSprite()
    {
        return gunSprite;
    }
}
