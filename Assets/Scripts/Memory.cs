using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    [SerializeField] bool learn = true;

    private List<float> xMoves;
    private List<float> times;
    private List<bool> jumps;

    private int timesCounter = 0;
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        xMoves = new List<float>();
        jumps = new List<bool>();
        times = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetClone();
        }

        if (learn)
        {
            bool jump = false;
            if (Input.GetButtonDown("Jump")) {
                GetComponent<CharacterController2D>().Jump();
                jump = true;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                GetComponent<CharacterController2D>().Move(1f);
                Add(1f, jump);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                GetComponent<CharacterController2D>().Move(-1f);
                Add(-1f, jump);
            } else
            {
                GetComponent<CharacterController2D>().Move(0f);
                Add(0f, jump);
            }

        } else {
            while (timesCounter < times.Count && times[timesCounter] < time)
            {
                GetComponent<CharacterController2D>().Move(xMoves[timesCounter]);
                if (jumps[timesCounter]) GetComponent<CharacterController2D>().Jump();
                timesCounter++;
            }
        }
    }

    public void Add(float movement, bool jump)
    {
        if (learn)
        {
            if (xMoves.Count == 0 || xMoves[xMoves.Count - 1] != movement || jumps.Count == 0 || jumps[jumps.Count - 1] != jump)
            {
                xMoves.Add(movement);
                jumps.Add(jump);
                times.Add(time);
            }
        }
        
    }

    public void SetClone()
    {
        learn = false;
        time = 0;
        timesCounter = 0;
    }

    public bool IsControlled()
    {
        return learn;
    }


}
