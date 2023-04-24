using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] bool fixedBackgroundSize = true;

    [SerializeField] List<Sprite> backgroundImages = new List<Sprite>();

    private Image image;

    private Canvas canvas;

    void Awake()
    {
        image = GetComponent<Image>();
        canvas = GetComponentInParent<Canvas>();
        SetRandomBackgroundImage();
        if(fixedBackgroundSize) canvas.renderMode = RenderMode.WorldSpace;
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
