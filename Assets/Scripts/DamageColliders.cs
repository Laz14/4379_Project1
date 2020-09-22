using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(ScreenFlash))]
public class DamageColliders : MonoBehaviour
{
    [SerializeField] int _damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Health>() != null)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(_damage);
            this.gameObject.GetComponent<ScreenFlash>().Flash();
        }
    }
}
