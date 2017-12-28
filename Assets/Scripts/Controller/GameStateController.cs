using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    private EnemyController enemyController;
    private void Awake()
    {
        Input.multiTouchEnabled = true;
        enemyController = GameFacade.GetInstance().EnemyController;
    }

    private IEnumerator Start()
    {
        while(true)
        {
            yield return StartCoroutine(PlayPhase());
            yield return StartCoroutine(EndPhase());
        }
    }

    private IEnumerator PlayPhase()
    {
        Debug.Log("[GameStateController] PlayPhase!");
        yield return StartCoroutine(enemyController.Execute());
    }

    private IEnumerator EndPhase()
    {
        Debug.Log("[GameStateController] EndPhase!");
        yield return null;
    }
}
