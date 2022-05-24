using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableHorizontal : Movable
{

    [SerializeField] float leftSpeed, rightSpeed;
    [SerializeField] bool constantlyMoving, startsOn;
    [SerializeField] State startState;

    private Vector3[] positions;

    private Vector3 left, right;

    public enum State { left, right, movingLeft, movingRight }
    public State state;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        on = startsOn;
        state = startState;
        left = transform.GetChild(0).transform.position;
        right = transform.GetChild(1).transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.zero;

        if (constantlyMoving && on)
        {
            if (state == State.movingLeft && transform.position.x <= left.x) state = State.left;
            else if (state == State.movingRight && transform.position.x >= right.x) state = State.right;

            switch (state)
            {
                case State.left:
                    {
                        state = State.movingRight;
                        break;
                    }
                case State.right:
                    {
                        state = State.movingLeft;
                        break;
                    }
                case State.movingLeft:
                    {
                        rb.velocity = Vector3.left * leftSpeed;
                        break;
                    }
                case State.movingRight:
                    {
                        rb.velocity = -Vector3.left * rightSpeed;
                        break;
                    }
            }
        }
        else if (!constantlyMoving)
        {
            if (state == State.movingLeft && transform.position.x <= left.x) state = State.left;
            else if (state == State.movingRight && transform.position.x >= right.x) state = State.right;

            if (state != State.right && (((startState == State.left && on) || (startState == State.right && !on))))
            {
                state = State.movingRight;
                rb.velocity = -Vector3.left * rightSpeed;
            }
            else if (state != State.left && (((startState == State.right && on) || (startState == State.left && !on))))
            {
                state = State.movingLeft;
                rb.velocity = Vector3.left * leftSpeed;
            }

        }
    }


}
