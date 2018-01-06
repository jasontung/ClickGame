using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private PlayerData playerData;
    private LevelData levelData;
    private ParticleSystem[] hitEffects = new ParticleSystem[3];
    private int hitEffectUseIndex;
    private void Awake()
    {
        playerData = GameFacade.GetInstance().playerData;
        levelData = GameFacade.GetInstance().levelData;
        RefreshPlayerData();
    }

    private void RefreshPlayerData()
    {
        LevelData.LevelSetting curLevelSetting = levelData.CurLevelSetting;
        playerData.attack = curLevelSetting.attack;
        for(int i = 0; i < hitEffects.Length; i++)
        {
            if (hitEffects[i] != null)
                Destroy(hitEffects[i].gameObject);
            hitEffects[i] = Instantiate(curLevelSetting.hitEffect);
        }
    }

    public void OnClickEnemy(EnemyBehavior enemy)
    {
        enemy.DoDamage(playerData.attack);
        ParticleSystem hitEffect = hitEffects[hitEffectUseIndex];
        hitEffect.transform.position = enemy.hitPoint.position;
        hitEffect.Stop();
        hitEffect.Play();
        hitEffectUseIndex = (int)Mathf.Repeat(hitEffectUseIndex + 1, hitEffects.Length);
    }
}
