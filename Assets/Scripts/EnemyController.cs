using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public Transform enemySpawnPoint;
    public IEnumerator Execute(EnemyData enemyData)
    {
        EnemyBehavior enemy = Instantiate(enemyData.enemyPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation);
        yield return StartCoroutine(enemy.Execute(enemyData));
    }
}
