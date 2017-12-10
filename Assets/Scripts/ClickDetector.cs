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
        playerController = GameObject.FindObjectOfType<PlayerController>();
        if (playerController == null)
            throw new Exception("PlayerController not set yet!");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        playerController.OnClick(enemyBehavior, eventData.pointerCurrentRaycast.worldPosition);
    }
}
