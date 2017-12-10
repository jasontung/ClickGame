using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyController))]
[RequireComponent(typeof(PlayerController))]
public class GameManager : MonoBehaviour
{
    private EnemyController enemyController;
    private PlayerController playerController;
    public StageData stageData;
   
    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        playerController = GetComponent<PlayerController>();
    }

    // Use this for initialization
    IEnumerator Start()
    {
        while (true)
        {
            yield return StartCoroutine(PlayPhase());
            yield return StartCoroutine(EndPhase());
        }
    }

    IEnumerator PlayPhase()
    {
        for (int i = 0; i < stageData.enemys.Length; i++)
        {
            yield return StartCoroutine(enemyController.Execute(stageData.enemys[i]));
        }
    }

    IEnumerator EndPhase()
    {
        yield return null;
    }
}
