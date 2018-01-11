using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateData
{
    public bool isFail;
    public StageData CurStageData
    {
        get
        {
            PlayerData playerData = GameFacade.GetInstance().playerData;
            return GameFacade.GetInstance().stageDatas[playerData.stageIndex];
        }
    }

    public void NextStage()
    {
        PlayerData playerData = GameFacade.GetInstance().playerData;
        StageData[] stageDatas = GameFacade.GetInstance().stageDatas;
        playerData.stageIndex = Mathf.Min(playerData.stageIndex + 1, stageDatas.Length - 1);
    }
}
