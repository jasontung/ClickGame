using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class LevelData : ScriptableObject {
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
            int lv = GameFacade.GetInstance().playerData.lv;
            int index = Mathf.Min(lv - 1, levelSettings.Length - 1);
            return levelSettings[index];
        }
    }

    public LevelSetting LastLevelSetting
    {
        get
        {
            int lv = GameFacade.GetInstance().playerData.lv;
            int index = Mathf.Max(lv - 2, 0);
            return levelSettings[index];
        }
    }
}
