using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]//attribute
public class EnemyBehavior : MonoBehaviour {
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

}
