using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    [Serializable]
    public class LevelSetting
    {
        public int exp;
        public int attack;
        public ParticleSystem hitEffect;
    }

    public LevelSetting[] levelSettings;
    private PlayerData PlayerData
    {
        get
        {
            return GameFacade.GetInstance().playerData;
        }
    }

    public LevelSetting CurLevelSetting
    {
        get
        {
            var index = Mathf.Min(PlayerData.lv - 1, levelSettings.Length - 1);
            return levelSettings[index];
        }
    }

    public LevelSetting LastLevelSetting
    {
        get
        {
            var index = Mathf.Max(PlayerData.lv - 2, 0);
            return levelSettings[index];
        }
    }
}
