using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform enemySpawnPoint;
    private StageData stageData;
    private GameUIController gameUIController;
    private void Awake()
    {
        stageData = GameDataContainer.GetInstance().stageData;
        gameUIController = GameManager.GetInstance().GameUIController;
    }

    public IEnumerator Execute()
    {
        for (int i = 0; i < stageData.enemys.Length; i++)
        {
            EnemyData enemyData = stageData.enemys[i];
            if (i == stageData.enemys.Length - 1)
            {
                yield return StartCoroutine(gameUIController.bossMsgEffect.Show());
            }
            EnemyBehavior enemy = Instantiate(enemyData.enemyPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation);
            yield return StartCoroutine(enemy.Execute(enemyData));
        }
    }
}
