using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyController))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GameUIController))]
[RequireComponent(typeof(AudioController))]
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
    public AudioController AudioController { private set; get; }

    public StageData[] stageDatas;
    public LevelData levelData;
    public PlayerData playerData;
    public DropItemData dropItemData;

    public StageData curStageData
    {
        get
        {
            return stageDatas[playerData.stageIndex];
        }
    }

    public bool IsFail
    {
        private set;
        get;
    }

    private void Awake()
    {
        GetInstance();
    }

    private void Initialize()
    {
        EnemyController = GetComponent<EnemyController>();
        PlayerController = GetComponent<PlayerController>();
        GameUIController = GetComponent<GameUIController>();
        AudioController = GetComponent<AudioController>();
        Input.multiTouchEnabled = true;
        LoadSaveData();
    }

    public void Save()
    {
        GameDataBase.Save(typeof(PlayerData).Name, playerData);
    }

    public void LoadSaveData()
    {
        playerData = GameDataBase.Load<PlayerData>(typeof(PlayerData).Name);
    }

    [ContextMenu("Clear Save Data")]
    public void Clear()
    {
        GameDataBase.Clear();
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
        AudioController.Stop();
        if (IsFail)
        {
            yield return StartCoroutine(GameUIController.stageFailEffect.Show());
        }
        else
        {
            yield return StartCoroutine(GameUIController.stageClearEffect.Show());
            playerData.stageIndex = Mathf.Min(playerData.stageIndex + 1, stageDatas.Length - 1);
        }
        IsFail = false;
    }

    public void OnTimeUp()
    {
        IsFail = true;
    }

    private void OnApplicationPause(bool pause)
    {
        Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
