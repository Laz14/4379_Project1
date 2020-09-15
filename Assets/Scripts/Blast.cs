using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : Ability
{
    float _blastForce = 500;
    float _blastRadius = 7;

    public override void Use(Transform origin)
    {
        // Debug.Log("BLAST");
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, _blastRadius);
        foreach (Collider c in hitColliders)
        {
            Debug.Log(c.gameObject.name);
            if (c.gameObject.tag == "Blastable")
            {
                Vector3 direction = -(transform.position - c.transform.position).normalized;

                // make closer things get pushed stronger
                float percentPower = (7 - Vector3.Distance(this.transform.position, c.transform.position)) / _blastRadius;
                // Debug.Log((7- Vector3.Distance(this.transform.position, c.transform.position)) + "   " + percentPower);
                c.attachedRigidbody?.AddForce(direction * _blastForce * percentPower);
            }
        }
    }
}
