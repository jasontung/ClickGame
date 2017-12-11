using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private PlayerData playerData;
    private LevelData levelData;
    private ParticleSystem hitEffect;
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
        playerData = GameDataContainer.GetInstance().playerData;
        levelData = GameDataContainer.GetInstance().levelData;
        gameUIController = GameManager.GetInstance().GameUIController;
    }

    public void OnEnable()
    {
        RefreshPlayerData();
    }

    public void RefreshPlayerData()
    {
        Destroy(hitEffect);
        hitEffect = Instantiate(CurLevelSetting.hitEffect);
        gameUIController.UpdateAttack(playerData.attack);
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
        gameUIController.UpdateExpSlider(playerData.exp);
        if (playerData.exp >= CurLevelSetting.exp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        playerData.lv = Mathf.Min(playerData.lv + 1, levelData.levelSettings.Length);
        playerData.attack = CurLevelSetting.attack;
        gameUIController.UpdateAttack(playerData.attack);
        gameUIController.UpdateLv(playerData.lv);
        gameUIController.UpdateExpSlider(playerData.exp, LastLevelSetting.exp, CurLevelSetting.exp);
        StartCoroutine(gameUIController.levelUpEffect.Show());
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
