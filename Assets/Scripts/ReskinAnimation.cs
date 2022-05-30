
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReskinAnimation : MonoBehaviour
{
    [SerializeField] string defaultColor = "Blue";
    [SerializeField] string animationColor;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        string spriteName = sr.sprite.name;
        string newSpriteName = spriteName.Replace(defaultColor, animationColor);
        Sprite[] sprites = Resources.LoadAll<Sprite>("Animations/" + newSpriteName.Split("_")[0]);
        int newSpriteIndex = System.Array.FindIndex(sprites, item => item.name == newSpriteName);

        if (newSpriteIndex >= 0) sr.sprite = sprites[newSpriteIndex];
        else
        {
            Debug.Log("Sprite not found: " + "Animations/" + newSpriteName);
        }
    }

    public void SetAnimationColor(string name)
    {
        animationColor = name;
    }

}
