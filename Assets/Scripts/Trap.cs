using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Memory memory = collision.GetComponent<Memory>();
        if (memory != null) memory.Die();
    }
}
