using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour {

    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }
   
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position);
    }
}
