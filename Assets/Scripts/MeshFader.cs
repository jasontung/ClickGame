using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshFader : MonoBehaviour
{
    public Renderer fadeRenderer;
    public float speed = 1f;

    public IEnumerator FadeIn()
    {
        StopCoroutine(FadeOut());
        var color = fadeRenderer.material.color;
        color.a = 0;
        fadeRenderer.material.color = color;
        while (color.a < 1f)
        {
            color.a += Time.deltaTime * speed;
            color.a = Mathf.Min(1, color.a);
            fadeRenderer.material.color = color;
            yield return null;
        }
    }
   
    public IEnumerator FadeOut()
    {
        StopCoroutine(FadeIn());
        var color = fadeRenderer.material.color;
        color.a = 1;
        fadeRenderer.material.color = color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime * speed;
            color.a = Mathf.Max(0, color.a);
            fadeRenderer.material.color = color;
            yield return null;
        }
    }
}
