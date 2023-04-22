using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour
{
    [SerializeField] private float _hitKillMagnitude = 1f;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("BirdPlayer") && col.relativeVelocity.magnitude > _hitKillMagnitude)
        {
            foreach (var contact in col.contacts)
            {
                Debug.Log("Hit normal "+ contact.normal + " "+ Vector3.Dot(contact.normal, Vector3.up));
                if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
                {
                    col.gameObject.GetComponent<BirdMovement>().Kill();
                    break;
                }
            }
        }
    }
}
