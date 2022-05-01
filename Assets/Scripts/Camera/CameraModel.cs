using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModel : MonoBehaviour
{
    public Camera cam;

    public GameObject player;
    
    public float percentAlong;
    public float maxMove;
    public float moveSpeed;

    public int quadrant;
    
    public Vector3 mousePos;
    public Vector3 moveDir;
    public Vector3 center;
    public Vector3 mouseDir;

    public Vector2 intersect;

    public ref Camera Cam(){
        return ref this.cam;
    }

    public ref GameObject Player(){
        return ref this.player;
    }

    public ref float PercentAlong(){
        return ref this.percentAlong;
    }

    public ref float MaxMove(){
        return ref this.maxMove;
    }

    public ref float MoveSpeed(){
        return ref this.moveSpeed;
    }

    public ref int Quadrant(){
        return ref this.quadrant;
    }

    public ref Vector3 MousePos(){
        return ref this.mousePos;
    }

    public ref Vector3 MoveDir(){
        return ref this.moveDir;
    }

    public ref Vector3 Center(){
        return ref this.center;
    }

    public ref Vector3 MouseDir(){
        return ref this.mouseDir;
    }

    public ref Vector2 Intersect(){
        return ref this.intersect;
    }
}
