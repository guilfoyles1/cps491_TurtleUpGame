using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    public int skinNr;
    public Skins[] skins;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SkinChoice()
    {

    }
}

[System.Serializable]
public struct Skins
{
    public Sprite[] sprites;
}