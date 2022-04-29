using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockModel : MonoBehaviour
{
    public string room;

    public bool locked;

    public Vector3Int direction;

    public ref string Room(){
        return ref this.room;
    }

    public ref bool Locked(){
        return ref this.locked;
    }

    public ref Vector3Int Direction(){
        return ref this.direction;
    }
}
