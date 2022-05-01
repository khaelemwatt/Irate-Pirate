using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModel : MonoBehaviour
{
    public string gun;

    public Sprite gunSprite;

    public ref string Gun(){
        return ref this.gun;
    }

    public ref Sprite GunSprite(){
        return ref this.gunSprite;
    }
}
