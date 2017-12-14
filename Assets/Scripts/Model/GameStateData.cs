using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class GameStateData
{
    public bool isFail;
    private PlayerData PlayerData
    {
        get
        {
            return GameFacade.GetInstance().playerData;
        }
    }
    private StageData[] StageDatas
    {
        get
        {
            return GameFacade.GetInstance().stageDatas;
        }
    }
    public StageData CurStageData
    {
        get
        {
            return StageDatas[PlayerData.stageIndex];
        }
    }

    public void NextStage()
    {
        PlayerData.stageIndex = Mathf.Min(PlayerData.stageIndex + 1, StageDatas.Length - 1);
    }
}
