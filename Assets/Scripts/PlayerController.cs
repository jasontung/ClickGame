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

    public LevelData.LevelSetting CurLevelSetting
    {
        get
        {
            var index = Mathf.Min(playerData.lv - 1, levelData.levelSettings.Length - 1);
            return levelData.levelSettings[index];
        }
    }

    public LevelData.LevelSetting LastLevelSetting
    {
        get
        {
            var index = Mathf.Max(playerData.lv - 2, 0);
            return levelData.levelSettings[index];
        }
    }

    private void Awake()
    {
        playerData = GameManager.GetInstance().playerData;
        levelData = GameManager.GetInstance().levelData;
        gameUIController = GameManager.GetInstance().GameUIController;
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
            hitEffects[i] = Instantiate(CurLevelSetting.hitEffect);
        }
        gameUIController.UpdateAttack(CurLevelSetting.attack);
        gameUIController.UpdateCoin(playerData.coin);
        gameUIController.UpdateLv(playerData.lv);
        gameUIController.UpdateExpSlider(playerData.exp, LastLevelSetting.exp, CurLevelSetting.exp);
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
        gameUIController.UpdateExpSlider(playerData.exp, LastLevelSetting.exp, CurLevelSetting.exp);
        if (playerData.exp >= CurLevelSetting.exp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        playerData.lv = Mathf.Min(playerData.lv + 1, levelData.levelSettings.Length);
        playerData.attack = CurLevelSetting.attack;
        RefreshPlayerData();
        StartCoroutine(gameUIController.levelUpEffect.Show());
    }

    public void OnClick(EnemyBehavior enemy)
    {
        if (GameManager.GetInstance().IsFail)
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
