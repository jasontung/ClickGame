using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(EnemyBehavior))]
public class ClickDetector : MonoBehaviour, IPointerDownHandler
{
    private PlayerController playerController;
    private EnemyBehavior enemyBehavior;
    private void Awake()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
        playerController = GameManager.GetInstance().PlayerController;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        playerController.OnClick(enemyBehavior, eventData.pointerCurrentRaycast.worldPosition);
    }
}
