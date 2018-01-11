using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyController))]
[RequireComponent(typeof(GameStateController))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GameUIController))]
public class GameFacade : MonoBehaviour {
    private static GameFacade instance;
    public static GameFacade GetInstance()
    {
        if(instance == null)
        {
            instance = GameObject.FindObjectOfType<GameFacade>();
            if (instance == null)
                throw new System.Exception("GameFacade不存在於場景中，請在場景中添加");
            instance.Initialize();
        }
        return instance;
    }
    #region Controller
    public EnemyController EnemyController { private set; get; }
    public GameStateController GameStateController { private set; get; }
    public PlayerController PlayerController { private set; get; }
    public GameUIController GameUIController { private set; get; }
    #endregion
    #region Model
    public StageData[] stageDatas;
    public PlayerData playerData;
    public GameStateData gameStateData;
    public LevelData levelData;
    #endregion
    private void Initialize()
    {
        Debug.Log("[GameFacade] Initialize!");
        EnemyController = GetComponent<EnemyController>();
        GameStateController = GetComponent<GameStateController>();
        PlayerController = GetComponent<PlayerController>();
        GameUIController = GetComponent<GameUIController>();
        playerData = GameDataBase.Load<PlayerData>(typeof(PlayerData).Name);
        gameStateData = new GameStateData();
    }

    [ContextMenu("Clear Data")]
    private void Clear()
    {
        GameDataBase.Clear();
    }

    private void Awake()
    {
        GetInstance();
    }

    private void OnApplicationPause(bool pause)
    {
        Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Save()
    {
        GameDataBase.Save(typeof(PlayerData).Name, playerData);
    }
}
