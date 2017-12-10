using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu]
public class LevelData : ScriptableObject {
    [Serializable]
    public class LevelSetting
    {
        public int exp;
        public int attack;
        public ParticleSystem hitEffect;
    }

    public LevelSetting[] levelSettings;
	
}
