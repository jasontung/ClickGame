using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropItemCoin : DropItemBehavior
{
    private DropItemData.DropItemSetting itemSetting;
    [SerializeField]
    private AudioClip collectSound;

    public override void SetData(DropItemData.DropItemSetting setting)
    {
        itemSetting = setting;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(Collect());
    }

    private IEnumerator Collect()
    {
        col.enabled = false;
        playerController.AddCoin(itemSetting.coinAmount);
        audioSource.clip = collectSound;
        audioSource.Play();
        yield return StartCoroutine(meshFader.FadeOut());
        Destroy(gameObject);
    }
   
}
