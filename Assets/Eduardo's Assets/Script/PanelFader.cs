using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelFader : MonoBehaviour
{
    public float fadeTime = 1f;

    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(FadePanel());
    }

    private IEnumerator FadePanel()
    {
        float timer = 0f;

        while (timer < fadeTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeTime);
            canvasGroup.alpha = alpha;

            timer += Time.deltaTime;
            yield return null;
        }
    }
}