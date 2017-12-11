using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(MeshFader))]
[RequireComponent(typeof(Collider))]
public abstract class DropItemBehavior : MonoBehaviour, IPointerDownHandler
{
    protected Rigidbody rigid;
    protected AudioSource audioSource;
    protected PlayerController playerController;
    protected MeshFader meshFader;
    protected Collider col;
    public float explosionForce = 250f;
    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        playerController = GameManager.GetInstance().PlayerController;
        meshFader = GetComponent<MeshFader>();
        col = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        rigid.AddExplosionForce(explosionForce, transform.position + UnityEngine.Random.insideUnitSphere, 1f);
    }

    public abstract void SetData(DropItemData.DropItemSetting setting);
    public abstract void OnPointerDown(PointerEventData eventData);
}
