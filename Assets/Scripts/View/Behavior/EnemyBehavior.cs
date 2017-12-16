using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(MeshFader))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(DropableBehavior))]
public class EnemyBehavior : MonoBehaviour
{
    private Animator animator;
    private HealthComponent healthBehavior;
    private MeshFader meshFader;
    private AudioSource audioSource;
    private DropableBehavior dropableBehavior;
    private PlayerController playerController;
    private GameUIController gameUIController;
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
            return healthBehavior.IsOver;
        }
    }

    private void Awake()
    {
        healthBehavior = GetComponent<HealthComponent>();
        audioSource = GetComponent<AudioSource>();
        meshFader = GetComponent<MeshFader>();
        animator = GetComponent<Animator>();
        dropableBehavior = GetComponent<DropableBehavior>();
        playerController = GameFacade.GetInstance().PlayerController;
        gameUIController = GameFacade.GetInstance().GameUIController;
        gameStateData = GameFacade.GetInstance().gameStateData;
    }

    private void OnEnable()
    {
        StartCoroutine(meshFader.FadeIn());
    }

    public IEnumerator Execute(EnemyData data)
    {
        healthBehavior.Init(data.health);
        if (data.defeatTimeLimit > 0)
            StartCoroutine(StartCountDownTimer(data.defeatTimeLimit));
        while (IsDead == false)
        {
            if (gameStateData.isFail)
                yield break;
            yield return null;
        }
        animator.SetTrigger("die");
        audioSource.clip = deadClip;
        audioSource.Play();
        dropableBehavior.CheckDropItem(data.willDropItemId, data.dropProbability);
        playerController.AddExp(data.health);
        gameUIController.countDownTimerEffect.Hide();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return StartCoroutine(meshFader.FadeOut());
    }

    private IEnumerator StartCountDownTimer(float remainTime)
    {
        yield return StartCoroutine(gameUIController.countDownTimerEffect.Show(remainTime));
        if (IsDead)
            yield break;
        gameStateData.isFail = true;
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
