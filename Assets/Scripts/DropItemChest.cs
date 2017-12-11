using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Animator))]
public class DropItemChest : DropItemBehavior {
    private DropItemData.DropItemSetting itemSetting;
    [SerializeField]
    private AudioClip collectSound;
    private Animator animator;
    private bool isCollected;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (isCollected)
            return;
        StartCoroutine(Collect());
    }

    public override void SetData(DropItemData.DropItemSetting setting)
    {
        itemSetting = setting;
    }

    private IEnumerator Collect()
    {
        isCollected = true;
        animator.SetTrigger("open");
        audioSource.clip = collectSound;
        audioSource.Play();
        playerController.AddCoin(itemSetting.coinAmount);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        col.enabled = false;
        yield return StartCoroutine(meshFader.FadeOut());
        Destroy(gameObject);
    }
}
