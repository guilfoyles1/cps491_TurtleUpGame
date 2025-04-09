using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAnimationScript : MonoBehaviour
{
    public Image image;
    public List<Sprite> sprites;
    public float animSpeed = 1f; // Time per frame
    private int index;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= animSpeed)
        {
            timer = 0f;
            index = (index + 1) % sprites.Count; // Loops the animation
            image.sprite = sprites[index];
        }
    }
}
