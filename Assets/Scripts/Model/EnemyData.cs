using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyData
{
    public int health;
    public int willDropItemID;
    public float dropProbability;
    public float defeatTimeLimit;
    public EnemyBehavior enemyPrefab;
}
