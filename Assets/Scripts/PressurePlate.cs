using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] Movable[] controlledMovables;
    [SerializeField] Sprite onSprite, offSprite;

    public List<Collider2D> collisions;
    private Vector3 startPosition;

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
        if (on) GetComponent<SpriteRenderer>().sprite = onSprite;
        else GetComponent<SpriteRenderer>().sprite = offSprite;

        foreach (Movable movable in controlledMovables)
        {
            movable.SetOn(on);
        }
    }

}
