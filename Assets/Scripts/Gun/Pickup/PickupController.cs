using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    InventoryController inventoryController;

    //#--------------------# UPDATE #--------------------#
    void Update()
    {
        //Rotate in circles
        //transform.Rotate(Vector3.up * (Time.deltaTime * rotationSpeed));

        //Bob up and down
        float sinYValue = Mathf.Sin(Time.time * 3.75f) * 0.02f;
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + sinYValue, 13);
    }
}
