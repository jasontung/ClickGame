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
            StageData[] stageDatas = GameFacade.GetInstance().stageDatas;
            return stageDatas[playerData.stageIndex];
        }
    }	
}
