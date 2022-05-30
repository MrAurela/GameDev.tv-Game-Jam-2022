using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip bg;
    [SerializeField] AudioClip death, win, jump, button, pull;

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

    public void PlayDeath()
    {
        GetComponent<AudioSource>().PlayOneShot(death);
    }

    public void PlayWin()
    {
        GetComponent<AudioSource>().PlayOneShot(win);
    }

    public void PlayJump()
    {
        GetComponent<AudioSource>().PlayOneShot(jump);
    }
    public void PlayButtonPressed()
    {
        GetComponent<AudioSource>().PlayOneShot(button);
    }

    public void PlayObjectMoving()
    {
        GetComponent<AudioSource>().PlayOneShot(pull);
    }
}
