using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MeshFader))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(HealthComponent))]
public class EnemyBehavior : MonoBehaviour
{
    private MeshFader meshFader;
    private Animator animator;
    private AudioSource audioSource;
    private HealthComponent healthComponent;
    private PlayerController playerController;
    [SerializeField]
    private AudioClip hurtClip;
    [SerializeField]
    private AudioClip deadClip;
    public Transform hitPoint;
    public bool IsDead
    {
        get
        {
            return healthComponent.IsOver;
        }
    }
    #region Public Method
    /// <summary>
    /// 請呼叫我 開始敵人生命邏輯
    /// </summary>
    /// <returns></returns>
    public IEnumerator Execute(EnemyData data)
    {
        healthComponent.Init(data.health);
        while (IsDead == false)
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
        healthComponent.Hurt(attack);
        animator.SetTrigger("hurt");
        audioSource.clip = hurtClip;
        audioSource.Play();
    }
    #endregion

    #region Private Method
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

    private void Update()
    {
        if (IsDead)
            return;
#if UNITY_EDITOR
        if (Input.GetButtonDown("Fire1"))
            playerController.OnClick(this);
#else
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    playerController.OnClick(this);
                }
            }
        }
#endif
    }
#endregion
}
