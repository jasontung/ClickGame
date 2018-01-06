using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    [System.Serializable]
    public class LevelSetting
    {
        public int exp;
        public int attack;
        public ParticleSystem hitEffect;
    }

    public LevelSetting[] levelSettings;

    public LevelSetting CurLevelSetting
    {
        get
        {
            PlayerData playerData = GameFacade.GetInstance().playerData;
            int index = Mathf.Min(playerData.lv - 1, levelSettings.Length - 1);
            return levelSettings[index];
        }
    }
}
