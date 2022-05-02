using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockController : MonoBehaviour
{   
    DockModel dockModel;
    PlayerController playerController;

    public void AssignDirection(string dir)
    {
        dockModel = gameObject.GetComponent<DockModel>();
        ref Vector3Int direction = ref dockModel.Direction();
        switch(dir)
        {
            case "up":
                direction = new Vector3Int(0, 3, 0);
                break;
            case "down":
                direction = new Vector3Int(0, -3, 0);
                break;
            case "left":
                direction = new Vector3Int(-3, 0, 0);
                break;
            case "right":
                direction = new Vector3Int(3, 0, 0);
                break;
        }
    }

    public void AssignRoom(string roomToAssign){
        ref string room = ref dockModel.Room();
        room = roomToAssign;
    }

    //#--------------------# ONCOLLISIONENTER2D #--------------------#
    public void OnCollisionEnter2D(Collision2D collision)
    {
        ref bool locked = ref dockModel.Locked();
        ref Vector3Int direction = ref dockModel.Direction();
        GameObject player = collision.collider.gameObject.transform.parent.gameObject;
        playerController = player.GetComponent<PlayerController>();
        if(player.CompareTag("Player") && locked == false)
        {
            player.transform.position += direction;            
            playerController.CollideWithDock(gameObject);
        }
    }

    public void LockDock(){
        ref bool locked = ref dockModel.Locked();
        locked = true;
    }

    public void UnlockDock(){
        ref bool locked = ref dockModel.Locked();
        locked = false;
    }
}
