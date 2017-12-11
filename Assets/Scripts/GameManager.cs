using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyController))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GameUIController))]
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<GameManager>();
            if (instance == null) throw new Exception("GameManager不存在於場景中，請在場景中添加");
            instance.Initialize();
        }
        return instance;
    }
    public EnemyController EnemyController { private set; get; }
    public PlayerController PlayerController { private set; get; }
    public GameUIController GameUIController { private set; get; }

    private void Awake()
    {
        GetInstance();
    }

    private void Initialize()
    {
        EnemyController = GetComponent<EnemyController>();
        PlayerController = GetComponent<PlayerController>();
        GameUIController = GetComponent<GameUIController>();
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
        yield return StartCoroutine(EnemyController.Execute());
    }

    IEnumerator EndPhase()
    {
        yield return StartCoroutine(GameUIController.stageClearEffect.Show());
    }
}
