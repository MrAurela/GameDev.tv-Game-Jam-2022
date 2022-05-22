using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] Movable[] controlledMovables;

    public List<Collider2D> collisions;

    // Start is called before the first frame update
    void Start()
    {
        collisions = new List<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisions.Add(collision);
        SetOn(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisions.Remove(collision);
        if (collisions.Count == 0) SetOn(false);
    }

    public void SetOn(bool on)
    {
        if (on) transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
        else transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;

        foreach (Movable movable in controlledMovables)
        {
            movable.SetOn(on);
        }
    }
}