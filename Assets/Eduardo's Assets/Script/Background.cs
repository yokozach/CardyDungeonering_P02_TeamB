using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{

    public List<Sprite> backgroundImages = new List<Sprite>();

    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        SetRandomBackgroundImage();
    }

    public void SetRandomBackgroundImage()
    {
        if (backgroundImages.Count > 0)
        {
            int randomIndex = Random.Range(0, backgroundImages.Count);
            image.sprite = backgroundImages[randomIndex];
        }
    }

}
