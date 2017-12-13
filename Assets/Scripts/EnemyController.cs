using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform enemySpawnPoint;
    private GameUIController gameUIController;
    private AudioController audioController;

    private void Awake()
    {
        gameUIController = GameManager.GetInstance().GameUIController;
        audioController = GameManager.GetInstance().AudioController;
    }

    public IEnumerator Execute()
    {
        StageData stageData = GameManager.GetInstance().curStageData;
        audioController.PlayBattleSound();
        for (int i = 0; i < stageData.enemys.Length; i++)
        {
            EnemyData enemyData = stageData.enemys[i];
            if (i == stageData.enemys.Length - 1)
            {
                audioController.PlayBossSound();
                yield return StartCoroutine(gameUIController.bossMsgEffect.Show());
            }
            EnemyBehavior enemy = Instantiate(enemyData.enemyPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation);
            yield return StartCoroutine(enemy.Execute(enemyData));
            Destroy(enemy.gameObject);
        }
        audioController.Stop();
    }
}
