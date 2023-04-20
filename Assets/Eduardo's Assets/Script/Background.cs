using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    public List<Sprite> backgroundImages = new List<Sprite>();

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetRandomBackgroundImage();
    }

    public void SetRandomBackgroundImage()
    {
        if (backgroundImages.Count > 0)
        {
            int randomIndex = Random.Range(0, backgroundImages.Count);
            spriteRenderer.sprite = backgroundImages[randomIndex];
        }
    }

}
