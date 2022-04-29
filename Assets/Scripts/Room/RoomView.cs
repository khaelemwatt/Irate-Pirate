using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomView : MonoBehaviour
{
    public Tilemap map;
    public Tilemap details;
    public Tilemap collision;

    public ref Tilemap Map(){
        return ref this.map;
    }

    public ref Tilemap Details(){
        return ref this.details;
    }

    public ref Tilemap Collision(){
        return ref this.collision;
    }
}
