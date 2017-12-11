using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataContainer : MonoBehaviour {
    private static GameDataContainer instance;
    public static GameDataContainer GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<GameDataContainer>();
            if (instance == null) throw new Exception("GameDataContainer不存在於場景中，請在場景中添加");
        }
        return instance;
    }

    public StageData stageData;
    public LevelData levelData;
    public PlayerData playerData;
    public DropItemData dropItemData;
}
