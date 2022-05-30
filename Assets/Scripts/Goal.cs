using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] bool death = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
           
            if (!death)
            {
                FindObjectOfType<SoundManager>().PlayWin();
                SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCount);
            }
            else
            {
                FindObjectOfType<SoundManager>().PlayDeath();
                SceneManager.LoadScene((6));
            }
        }
    }
}
