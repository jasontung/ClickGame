using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(HealthBehavior))]
[RequireComponent(typeof(MeshFader))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(DropableBehavior))]
public class EnemyBehavior : MonoBehaviour
{
    public const string animDeadClipName = "die";
    public const string animHurtClipName = "hurt";
    private Animator animator;
    private HealthBehavior healthBehavior;
    private MeshFader meshFader;
    private AudioSource audioSource;
    private DropableBehavior dropableBehavior;
    private EnemyData enemyData;
    [SerializeField]
    private AudioClip hurtClip;
    [SerializeField]
    private AudioClip deadClip;

    public bool isDead
    {
        private set;
        get;
    }

    private void Awake()
    {
        healthBehavior = GetComponent<HealthBehavior>();
        audioSource = GetComponent<AudioSource>();
        meshFader = GetComponent<MeshFader>();
        animator = GetComponent<Animator>();
        dropableBehavior = GetComponent<DropableBehavior>();
    }

    public IEnumerator Execute(EnemyData data)
    {
        enemyData = data;
        healthBehavior.Init(enemyData.health);
        while (healthBehavior.isDead == false)
        {
            yield return null;
        }
        isDead = true;
        animator.Play(animDeadClipName);
        audioSource.clip = deadClip;
        audioSource.Play();
        dropableBehavior.CheckDropItem(enemyData.willDropItemId, enemyData.dropProbability);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return StartCoroutine(meshFader.FadeOut());
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        StartCoroutine(meshFader.FadeIn());
    }

    public void DoDamage(int attack)
    {
        healthBehavior.Hurt(attack);
        animator.Play(animHurtClipName,0,0);
        audioSource.clip = hurtClip;
        audioSource.Play();
    }
}
