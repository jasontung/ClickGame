using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour {
    private EnemyController enemyController;
    private AudioController audioController;
    private GameUIController gameUIController;
    private GameStateData gameStateData;
    
    private void Awake()
    {
        Input.multiTouchEnabled = true;
        gameStateData = GameFacade.GetInstance().gameStateData;
        audioController = GameFacade.GetInstance().AudioController;
        enemyController = GameFacade.GetInstance().EnemyController;
        gameUIController = GameFacade.GetInstance().GameUIController;
    }

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
        yield return StartCoroutine(enemyController.Execute());
    }

    IEnumerator EndPhase()
    {
        audioController.Stop();
        if (gameStateData.isFail)
        {
            yield return StartCoroutine(gameUIController.stageFailEffect.Show());
        }
        else
        {
            yield return StartCoroutine(gameUIController.stageClearEffect.Show());
            gameStateData.NextStage();
        }
        gameStateData.isFail = false;
    }
}
