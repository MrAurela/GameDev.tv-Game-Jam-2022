using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip bg;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().clip = bg;
        GetComponent<AudioSource>().Play();

        if (FindObjectsOfType<SoundManager>().Length > 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
