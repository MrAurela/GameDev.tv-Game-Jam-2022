using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{

    [SerializeField] float upSpeed, downSpeed;
    [SerializeField] bool constantlyMoving, startsOn;
    [SerializeField] State startState;

    private Vector3[] positions;

    public bool on;
    private Vector3 up, down;

    public enum State { up, down, movingUp, movingDown }
    public State state;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        on = startsOn;
        state = startState;
        up = transform.GetChild(0).transform.position;
        down = transform.GetChild(1).transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.zero;
        if (!on) return;

        if (state == State.movingUp && transform.position.y >= up.y) state = State.up;
        else if (state == State.movingDown && transform.position.y <= down.y) state = State.down;
        else
        {
            Debug.Log(state + ", " + transform.position.y + ", " + up.y + ", " + down.y);
        }

        switch (state) {
            case State.up:
                {
                    state = State.movingDown;
                    break;
                }
            case State.down:
                {
                    state = State.movingUp;
                    break;
                }
            case State.movingUp:
                {
                    rb.velocity = Vector3.up * upSpeed;
                    break;
                }
            case State.movingDown:
                {
                    rb.velocity = -Vector3.up * downSpeed;
                    break;
                }

        }
    }

    public void SetOn(bool on)
    {
        this.on = on;
    }

}
