using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MeshFader))]
[RequireComponent(typeof(AudioSource))]
public class EnemyBehavior : MonoBehaviour {
    private MeshFader meshFader;
    private Animator animator;
    private AudioSource audioSource;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        meshFader = GetComponent<MeshFader>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StartCoroutine(meshFader.FadeIn());
    }
    
    [ContextMenu("Test Damage")]
    public void Update()
    {
        if(Input.GetButtonDown("Fire1"))
            DoDamage(10);
    }
    public void DoDamage(int attack)
    {
        animator.SetTrigger("hurt");
        audioSource.Play();
    }
    public IEnumerator Execute()
    {
        yield return null;
    }
}
