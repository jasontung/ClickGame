using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshFader : MonoBehaviour {
    public Renderer[] fadeRenderers;
    public float speed = 1f;
	
    [ContextMenu("Test FadeIn")]
    private void TestFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    [ContextMenu("Test FadeOut")]
    private void TestFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeIn()
    {
        StopCoroutine(FadeOut());
        float alpha = 0;
        ChangeAlpha(alpha);
        while (alpha < 1)
        {
            alpha += Time.deltaTime * speed;
            alpha = Mathf.Min(1, alpha);
            ChangeAlpha(alpha);
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        StopCoroutine(FadeIn());
        float alpha = 1;
        ChangeAlpha(alpha);
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * speed;
            alpha = Mathf.Max(0, alpha);
            ChangeAlpha(alpha);
            yield return null;
        }
    }

    private void ChangeAlpha(float alpha)
    {
        for (int i = 0; i < fadeRenderers.Length; i++)
        {
            Color color = fadeRenderers[i].material.color;
            color.a = alpha;
            fadeRenderers[i].material.color = color;
        }
    }
}
