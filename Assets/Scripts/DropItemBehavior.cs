using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Rigidbody))]
public class DropItemBehavior : MonoBehaviour, IPointerDownHandler
{
    private Rigidbody rigid;
    private PlayerController playerController;
    public float explosionForce = 10f;
    private int amount;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
        if (playerController == null)
            throw new Exception("PlayerController not set yet!");
    }

    private void OnEnable()
    {
        rigid.AddExplosionForce(explosionForce, transform.position + UnityEngine.Random.insideUnitSphere, 1f);
    }

    public void SetData(int val)
    {
        amount = val;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        playerController.AddCoin(amount);
        Debug.Log("Click");
    }
}
