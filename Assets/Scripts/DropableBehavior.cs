using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropableBehavior : MonoBehaviour
{
    private DropItemController dropItemController;

    private void Awake()
    {
        dropItemController = GameObject.FindObjectOfType<DropItemController>();
        if (dropItemController == null)
            throw new Exception("DropItemData not set yet!");
    }

    public void CheckDropItem(int itemId, float dropProbability)
    {
        float randomNum = UnityEngine.Random.value;
        if (randomNum <= dropProbability)
        {
            var itemSetting = dropItemController.Get(itemId);
            DropItemBehavior dropItem = Instantiate(itemSetting.itemPrefab, transform.position + transform.up, transform.rotation);
            dropItem.SetData(itemSetting.coinAmount);
        }
    }
}
