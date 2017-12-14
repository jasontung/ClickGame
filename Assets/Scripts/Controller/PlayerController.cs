using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private PlayerData playerData;
    private LevelData levelData;
    private int hitEffectUseIndex;
    private ParticleSystem[] hitEffects = new ParticleSystem[3];
    private GameUIController gameUIController;
    private GameStateData gameStateData;

    private void Awake()
    {
        playerData = GameFacade.GetInstance().playerData;
        levelData = GameFacade.GetInstance().levelData;
        gameUIController = GameFacade.GetInstance().GameUIController;
        gameStateData = GameFacade.GetInstance().gameStateData;
    }

    public void OnEnable()
    {
        RefreshPlayerData();
    }

    public void RefreshPlayerData()
    {
        for(int i = 0; i < hitEffects.Length; i++)
        {
            if(hitEffects[i] != null)
                Destroy(hitEffects[i].gameObject);
            hitEffects[i] = Instantiate(levelData.CurLevelSetting.hitEffect);
        }
        gameUIController.UpdateAttack(levelData.CurLevelSetting.attack);
        gameUIController.UpdateCoin(playerData.coin);
        gameUIController.UpdateLv(playerData.lv);
        gameUIController.UpdateExpSlider(playerData.exp, levelData.LastLevelSetting.exp, levelData.CurLevelSetting.exp);
    }

    public void AddCoin(int amount)
    {
        playerData.coin += amount;
        gameUIController.UpdateCoin(playerData.coin);
    }

    public void AddExp(int amount)
    {
        if (playerData.lv >= levelData.levelSettings.Length)
            return;
        playerData.exp += amount;
        gameUIController.UpdateExpSlider(playerData.exp, levelData.LastLevelSetting.exp, levelData.CurLevelSetting.exp);
        if (playerData.exp >= levelData.CurLevelSetting.exp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        playerData.lv = Mathf.Min(playerData.lv + 1, levelData.levelSettings.Length);
        playerData.attack = levelData.CurLevelSetting.attack;
        RefreshPlayerData();
        StartCoroutine(gameUIController.levelUpEffect.Show());
    }

    public void OnClick(EnemyBehavior enemy)
    {
        if (gameStateData.isFail)
            return;
        if (enemy.IsDead)
            return;
        enemy.DoDamage(playerData.attack);
        ParticleSystem hitEffect = hitEffects[hitEffectUseIndex];
        hitEffect.transform.position = enemy.hitPoint.position;
        hitEffect.Stop();
        hitEffect.Play();
        hitEffectUseIndex = (int)Mathf.Repeat(hitEffectUseIndex + 1, hitEffects.Length);
    }
}
