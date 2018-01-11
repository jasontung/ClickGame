using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private PlayerData playerData;
    private LevelData levelData;
    private ParticleSystem[] hitEffects = new ParticleSystem[3];
    private int hitEffectUseIndex;
    private GameUIController gameUIController;
    private void Awake()
    {
        playerData = GameFacade.GetInstance().playerData;
        levelData = GameFacade.GetInstance().levelData;
        gameUIController = GameFacade.GetInstance().GameUIController;
    }

    private void OnEnable()
    {
        RefreshPlayerData();
    }

    private void RefreshPlayerData()
    {
        playerData.attack = levelData.CurLevelSetting.attack;
        for(int i = 0; i < hitEffects.Length; i++)
        {
            if (hitEffects[i] != null)
                Destroy(hitEffects[i].gameObject);
            hitEffects[i] = Instantiate(levelData.CurLevelSetting.hitEffect);
        }
        gameUIController.UpdateAttack(playerData.attack);
        gameUIController.UpdateLv(playerData.lv);
        int minExp = levelData.LastLevelSetting.exp;
        int maxExp = levelData.CurLevelSetting.exp;
        if (playerData.lv == 1)
            minExp = 0;
        gameUIController.UpdateExpSlider(playerData.exp, minExp, maxExp);
    }

    public void AddExp(int amount)
    {
        if (playerData.lv > levelData.levelSettings.Length)
            return;
        playerData.exp += amount;
        int minExp = levelData.LastLevelSetting.exp;
        int maxExp = levelData.CurLevelSetting.exp;
        if (playerData.lv == 1)
            minExp = 0;
        gameUIController.UpdateExpSlider(playerData.exp, minExp, maxExp);
        if (playerData.exp >= maxExp)
            LevelUp();
    }

    private void LevelUp()
    {
        Debug.Log("[PlayerController] Level Up!!");
        playerData.lv = Mathf.Min(playerData.lv + 1, levelData.levelSettings.Length);
        playerData.attack = levelData.CurLevelSetting.attack;
        RefreshPlayerData();
    }

    public void OnClick(EnemyBehavior enemy)
    {
        enemy.DoDamage(playerData.attack);
        ParticleSystem hitEffect = hitEffects[hitEffectUseIndex];
        hitEffect.transform.position = enemy.hitPoint.position;
        hitEffect.Stop();
        hitEffect.Play();
        hitEffectUseIndex = (int)Mathf.Repeat(hitEffectUseIndex + 1, hitEffects.Length);
    }
}
