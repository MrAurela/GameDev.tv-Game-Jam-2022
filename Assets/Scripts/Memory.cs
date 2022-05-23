using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    [SerializeField] bool learn = true;

    private List<float> xMoves;
    private List<float> times;
    private List<bool> jumps;
    private List<(float, Vector3, Vector3, Sprite)> path; //timestamp, position, scale, sprite;

    private int timesCounter = 0;
    private float time = 0f;

    private int tm = 0, tmMin = 0;

    // Start is called before the first frame update
    void Start()
    {
        xMoves = new List<float>();
        jumps = new List<bool>();
        times = new List<float>();
        path = new List<(float, Vector3, Vector3, Sprite)>();


    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        path.Add((time, transform.position, transform.localScale, GetComponent<SpriteRenderer>().sprite));

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetClone();
        }

        if (learn)
        {
            /*bool jump = false;
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
            }*/

        } else {
            /*while (timesCounter < times.Count && times[timesCounter] < time)
            {
                GetComponent<CharacterController2D>().Move(xMoves[timesCounter]);
                if (jumps[timesCounter]) GetComponent<CharacterController2D>().Jump();
                timesCounter++;
            }*/

            tm = tmMin;
            while (tm < path.Count && path[tm].Item1 <= time)
            {
                tm++;
                tmMin++;
            }
            if (tm == path.Count) Disactivate();
            else if (tm >= 1)
            {
                transform.position = path[tm - 1].Item2;
                transform.localScale = path[tm - 1].Item3;
                GetComponent<SpriteRenderer>().sprite = path[tm - 1].Item4;
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
        GetComponent<Player>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.5f);
        gameObject.layer = 11; //Ghost
        learn = false;
        time = 0;
        timesCounter = 0;
        tm = 0;
        tmMin = 0;

    }

    public bool IsControlled()
    {
        return learn;
    }

    public void Die()
    {
        Disactivate();
        if (IsControlled())
        {
            Spawn spawn = FindObjectOfType<Spawn>();
            spawn.AddCharacter(gameObject);
            spawn.StartSpawning();
        }
    }

    public void Disactivate()
    {
        gameObject.SetActive(false);
        //gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.5f);
        //gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);


    }


}
