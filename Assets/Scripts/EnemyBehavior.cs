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
    private Animator animator;
    private HealthBehavior healthBehavior;
    private MeshFader meshFader;
    private AudioSource audioSource;
    private DropableBehavior dropableBehavior;
    private PlayerController playerController;
    private GameUIController gameUIController;
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
        playerController = GameManager.GetInstance().PlayerController;
        gameUIController = GameManager.GetInstance().GameUIController;
    }

    public IEnumerator Execute(EnemyData data)
    {
        enemyData = data;
        healthBehavior.Init(enemyData.health);
        if (enemyData.defeatTimeLimit > 0)
            StartCoroutine(gameUIController.countDownTimerEffect.Show(enemyData.defeatTimeLimit));
        while (healthBehavior.isDead == false)
        {
            yield return null;
        }
        gameUIController.countDownTimerEffect.Hide();
        isDead = true;
        animator.SetTrigger("die");
        audioSource.clip = deadClip;
        audioSource.Play();
        dropableBehavior.CheckDropItem(enemyData.willDropItemId, enemyData.dropProbability);
        playerController.AddExp(enemyData.health);
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
        animator.SetTrigger("hurt");
        audioSource.clip = hurtClip;
        audioSource.Play();
    }
}
