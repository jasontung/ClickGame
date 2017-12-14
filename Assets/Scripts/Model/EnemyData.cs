using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class EnemyData
{
    public int health = 100;
    public int willDropItemId;
    public float dropProbability;
    public float defeatTimeLimit;
    public EnemyBehavior enemyPrefab;
}
