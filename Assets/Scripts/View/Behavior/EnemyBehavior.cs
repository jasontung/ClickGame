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
    private EnemyData enemyData;
    private GameStateData gameStateData;
    [SerializeField]
    private AudioClip hurtClip;
    [SerializeField]
    private AudioClip deadClip;
    public Transform hitPoint;

    public bool IsDead
    {
        get
        {
            return healthBehavior.isDead;
        }
    }

    private void Awake()
    {
        healthBehavior = GetComponent<HealthBehavior>();
        audioSource = GetComponent<AudioSource>();
        meshFader = GetComponent<MeshFader>();
        animator = GetComponent<Animator>();
        dropableBehavior = GetComponent<DropableBehavior>();
        playerController = GameFacade.GetInstance().PlayerController;
        gameStateData = GameFacade.GetInstance().gameStateData;
    }

    public IEnumerator Execute(EnemyData data)
    {
        enemyData = data;
        healthBehavior.Init(enemyData.health);
        while (IsDead == false)
        {
            if (gameStateData.isFail)
                yield break;
            yield return null;
        }
        animator.SetTrigger("die");
        audioSource.clip = deadClip;
        audioSource.Play();
        dropableBehavior.CheckDropItem(enemyData.willDropItemId, enemyData.dropProbability);
        playerController.AddExp(enemyData.health);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return StartCoroutine(meshFader.FadeOut());
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

    private void Update()
    {
        if (IsDead)
            return;
#if UNITY_EDITOR
        if (Input.GetButtonDown("Fire1"))
        {
            playerController.OnClick(this);
        }
#else
        if(Input.touchCount > 0)
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                if(Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    playerController.OnClick(this);
                }
            }
        }
#endif
    }
}
