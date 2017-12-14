using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyController))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GameUIController))]
[RequireComponent(typeof(AudioController))]
[RequireComponent(typeof(GameStateController))]
public class GameFacade : MonoBehaviour
{
    private static GameFacade instance;
    public static GameFacade GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<GameFacade>();
            if (instance == null) throw new Exception("GameManager不存在於場景中，請在場景中添加");
            instance.Initialize();
        }
        return instance;
    }
    public EnemyController EnemyController { private set; get; }
    public PlayerController PlayerController { private set; get; }
    public GameUIController GameUIController { private set; get; }
    public AudioController AudioController { private set; get; }
    public GameStateController GameStateController { private set; get; }
    public StageData[] stageDatas;
    public LevelData levelData;
    public PlayerData playerData;
    public DropItemData dropItemData;
    public GameStateData gameStateData;

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
        GameStateController = GetComponent<GameStateController>();
        gameStateData = new GameStateData();
        playerData = GameDataBase.Load<PlayerData>(typeof(PlayerData).Name);
    }

    [ContextMenu("Clear Save Data")]
    public void Clear()
    {
        GameDataBase.Clear();
    }

    private void OnApplicationPause(bool pause)
    {
        Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public void Save()
    {
        GameDataBase.Save(typeof(PlayerData).Name, playerData);
    }

}
