using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    [SerializeField]
    private Transform enemySpawnPoint;
    private GameStateData gameStateData;
    private void Awake()
    {
        gameStateData = GameFacade.GetInstance().gameStateData;
    }

    public IEnumerator Execute()
    {
        Debug.Log("[EnemyController] Execute!");
        StageData stageData = gameStateData.CurStageData;
        for(int i = 0; i < stageData.enemys.Length; i++)
        {
            EnemyData enemyData = stageData.enemys[i];
            EnemyBehavior enemy = Instantiate(enemyData.enemyPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation);
            yield return StartCoroutine(enemy.Execute(enemyData));
            Destroy(enemy.gameObject);
        }
    }
}
