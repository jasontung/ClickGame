using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropableBehavior : MonoBehaviour
{
    private DropItemData dropItemData;
    private void Awake()
    {
        dropItemData = GameDataContainer.GetInstance().dropItemData;
    }

    public void CheckDropItem(int itemId, float dropProbability)
    {
        float randomNum = UnityEngine.Random.value;
        if (randomNum <= dropProbability)
        {
            var itemSetting = dropItemData.Get(itemId);
            DropItemBehavior dropItem = Instantiate(itemSetting.itemPrefab, transform.position + transform.up, transform.rotation);
            dropItem.SetData(itemSetting);
        }
    }
}
