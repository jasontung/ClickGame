using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu]
public class DropItemData : ScriptableObject {
    [Serializable]
	public class DropItemSetting
    {
        public int id;
        public DropItemBehavior itemPrefab;
        public int coinAmount = 1;
    }
    public DropItemSetting[] dropItemSettings;

    public DropItemSetting Get(int id)
    {
        var setting = Array.Find(dropItemSettings, data => data.id == id);
        if (setting == null)
            throw new Exception("DropItemSetting can't find id with " + id);
        return setting;
    }
}
