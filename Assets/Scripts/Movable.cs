using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable: MonoBehaviour 
{
    public bool on;

    public void SetOn(bool on)
    {
        this.on = on;
    }

    public Vector2 GetVelocity()
    {
        return GetComponent<Rigidbody2D>().velocity;
    }

}
