using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(EnemyBehavior))]
public class ClickDetector : MonoBehaviour { 
    private PlayerController playerController;
    private EnemyBehavior enemyBehavior;
    private void Awake()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
        playerController = GameManager.GetInstance().PlayerController;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if(Input.GetButtonDown("Fire1"))
        {
            playerController.OnClick(enemyBehavior);
        }
#else
        if(Input.touchCount > 0)
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                if(Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    playerController.OnClick(enemyBehavior);
                }
            }
        }
#endif
    }
}
