using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshFader : MonoBehaviour
{
    public Renderer[] fadeRenderers;
    public float speed = 1f;

    public IEnumerator FadeIn()
    {
        StopCoroutine(FadeOut());
        float alpha = 0;
        for (int i = 0; i < fadeRenderers.Length; i++)
        {
            Color color = fadeRenderers[i].material.color;
            color.a = alpha;
            fadeRenderers[i].material.color = color;
        }
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * speed;
            alpha = Mathf.Min(1, alpha);
            for (int i = 0; i < fadeRenderers.Length; i++)
            {
                Color color = fadeRenderers[i].material.color;
                color.a = alpha;
                fadeRenderers[i].material.color = color;
            }
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        StopCoroutine(FadeIn());
        float alpha = 1;
        for (int i = 0; i < fadeRenderers.Length; i++)
        {
            Color color = fadeRenderers[i].material.color;
            color.a = alpha;
            fadeRenderers[i].material.color = color;
        }
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * speed;
            alpha = Mathf.Max(0, alpha);
            for (int i = 0; i < fadeRenderers.Length; i++)
            {
                Color color = fadeRenderers[i].material.color;
                color.a = alpha;
                fadeRenderers[i].material.color = color;
            }
            yield return null;
        }
    }
}
