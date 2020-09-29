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

        if (this.gameObject.GetComponent<Rigidbody>() != null)
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        if (this.gameObject.GetComponent<ThirdPersonMovement>() != null)
        {
            this.gameObject.GetComponent<ThirdPersonMovement>().ResetIdle();
        }

        if (this.gameObject.GetComponent<Health>() != null)
        {
            this.gameObject.GetComponent<Health>().RestoreFull();
        }

        if (this.gameObject.active == false)
        {
            this.gameObject.SetActive(true);
        }
    }
}
