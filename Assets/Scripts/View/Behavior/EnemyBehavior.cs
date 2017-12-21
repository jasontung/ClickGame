using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MeshFader))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(HealthComponent))]
public class EnemyBehavior : MonoBehaviour {
    private MeshFader meshFader;
    private Animator animator;
    private AudioSource audioSource;
    private HealthComponent healthComponent;
    [SerializeField]
    private AudioClip hurtClip;
    [SerializeField]
    private AudioClip deadClip;
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
    public IEnumerator Execute()
    {
        healthComponent.Init(100);
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
    }

    private void OnEnable()
    {
        StartCoroutine(meshFader.FadeIn());
    }

    [ContextMenu("Test Execute")]
    private void TestExecute()
    {
        StartCoroutine(Execute());
    }
    private void Update()
    {
        if (IsDead)
            return;
        if(Input.GetButtonDown("Fire1"))
            DoDamage(50);
    }
    #endregion
}
