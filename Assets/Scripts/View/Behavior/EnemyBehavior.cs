using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(MeshFader))]
[RequireComponent(typeof(Animator))]//attribute
public class EnemyBehavior : MonoBehaviour {
    private Animator animator;
    private MeshFader meshFader;
    private AudioSource audioSource;
    private HealthComponent healthComponent;
    [SerializeField]
    private AudioClip hurtClip;
    [SerializeField]
    private AudioClip deadClip;
    private PlayerController playerController;
    public Transform hitPoint;
    public bool IsDead
    {
        get
        {
            return healthComponent.IsOver;
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        meshFader = GetComponent<MeshFader>();
        audioSource = GetComponent<AudioSource>();
        healthComponent = GetComponent<HealthComponent>();
        playerController = GameFacade.GetInstance().PlayerController;
    }

    private void OnEnable()
    {
        StartCoroutine(meshFader.FadeIn());
    }

    public IEnumerator Execute(EnemyData enemyData)
    {
        healthComponent.Init(enemyData.health);
        while(IsDead == false)
        {
            yield return null;
        }
        animator.SetTrigger("die");
        audioSource.clip = deadClip;
        audioSource.Play();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return StartCoroutine(meshFader.FadeOut());
    }

    public void DoDamage(int attack)
    {
        animator.SetTrigger("hurt");
        audioSource.clip = hurtClip;
        audioSource.Play();
        healthComponent.Hurt(attack);
    }

    private void Update()
    {
        if (IsDead)
            return;
#if UNITY_EDITOR
        if(Input.GetButtonDown("Fire1"))
        {
            playerController.OnClickEnemy(this);
        }
#else
        if(Input.touchCount > 0)
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                if(Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    playerController.OnClickEnemy(this);
                }
            }
        }
#endif
    }
}
