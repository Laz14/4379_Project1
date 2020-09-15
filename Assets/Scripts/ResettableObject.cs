using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResettableObject : MonoBehaviour
{
    private Vector3 _originalPosition;

    private void Awake()
    {
        _originalPosition = this.transform.position;
    }

    public void ResetState()
    {
        this.transform.position = _originalPosition;
        //this.gameObject.GetComponent<Rigidbody>()?.velocity = Vector3.zero;
        //this.GetComponent<ThirdPersonMovement>()?.ResetIdle();
    }
}
