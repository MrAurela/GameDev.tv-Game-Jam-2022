using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject playerCharacter;
    [SerializeField] float delay;
    [SerializeField] string[] characterColors;
    //[SerializeField] int maxCharacters;

    public List<GameObject> characters;
    private float timer;
    private bool spawning = false;
    private int count;

    private int maxCharacters = 0;
    private int characterColorIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        maxCharacters = characterColors.Length;
        characters = new List<GameObject>();
        StartSpawning();
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawning && count <= characters.Count)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                if (count == characters.Count)
                {
                    GameObject instantiated = Instantiate(playerCharacter, transform.position, Quaternion.identity);
                    SpawnCharacter(instantiated, true);
                    count++;
                    timer = 0f;
                }
                else
                {
                    SpawnCharacter(characters[count], false);
                    count++;
                    timer = 0f;
                }
            }
        }
            
    }

    private GameObject SpawnCharacter(GameObject character, bool controllable)
    {
        character.transform.position = transform.position;
        if (!controllable)
        {
            character.GetComponent<Memory>().SetClone();
        } else
        {
            Debug.Log(characters.Count);
            character.GetComponent<ReskinAnimation>().SetAnimationColor(characterColors[characterColorIndex]);
            characterColorIndex = (characterColorIndex + 1) % maxCharacters;
        }
        character.SetActive(true);
        return character;
    }

    public void AddCharacter(GameObject character)
    {
        characters.Add(character);
    }

    public void StartSpawning()
    {
        //Restart all platform positions when level is restarted
        foreach (Movable plattform in FindObjectsOfType<Movable>())
        {
            plattform.Restart();
        }

        while (characters.Count >= maxCharacters)
        {
            Destroy(characters[0]);
            characters.RemoveAt(0);
        }

        foreach (GameObject spawned in characters)
        {
            //Disable old versions
            spawned.GetComponent<Memory>().Disactivate();
        }
        spawning = true;
        timer = 0f;
        count = 0;
    }
}
