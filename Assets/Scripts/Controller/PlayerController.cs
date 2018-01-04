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
