using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnFade : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f;
    public float holdDuration = 1.0f;
    public bool isFading;

    public void RespawnFadeEffect()
    {
        if (!isFading)
        {
            StartCoroutine(FadeOutIn());  
        }
    }

    private IEnumerator FadeOutIn()
    {
        isFading = true;
        yield return StartCoroutine(Fade(0f, 1f));

        yield return new WaitForSeconds(holdDuration);

        yield return StartCoroutine(Fade(1f, 0f));
        isFading = false;
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        Color color = fadeImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;
    }
}
