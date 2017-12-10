using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DropItemController : MonoBehaviour {
    [SerializeField]
    private DropItemData dropItemData;
    public DropItemData.DropItemSetting Get(int id)
    {
        var setting = Array.Find(dropItemData.dropItemSettings, data => data.id == id);
        if (setting == null)
            throw new Exception("DropItemSetting can't find id with " + id);
        return setting;
    }
}
