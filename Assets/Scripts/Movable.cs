using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable: MonoBehaviour 
{
    public bool on;
    private Vector3 startPosition;
    public void Awake()
    {
        startPosition = transform.position;
    }

    public void SetOn(bool on)
    {
        this.on = on;
    }

    public Vector2 GetVelocity()
    {
        return GetComponent<Rigidbody2D>().velocity;
    }

    public void Restart()
    {
        transform.position = startPosition;
    }

}
