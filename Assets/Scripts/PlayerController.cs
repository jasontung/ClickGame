using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;
    private ParticleSystem hitEffect;
    public LevelData levelData;
    public LevelData.LevelSetting curLevelSetting
    {
        get
        {
            var index = Mathf.Min(playerData.lv, levelData.levelSettings.Length - 1);
            return levelData.levelSettings[index];
        }
    }

    public void OnEnable()
    {
        RefreshPlayerData();
    }

    public void RefreshPlayerData()
    {
        Destroy(hitEffect);
        hitEffect = Instantiate(curLevelSetting.hitEffect);
    }

    public void AddCoin(int amount)
    {
        playerData.coin += amount;
    }

    public void OnClick(EnemyBehavior enemy, Vector3 hitPoint)
    {
        if (enemy.isDead)
            return;
        enemy.DoDamage(playerData.attack);
        hitEffect.transform.position = hitPoint;
        hitEffect.Play();
    }
}
