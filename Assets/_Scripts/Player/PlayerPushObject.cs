using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerPushObject : MonoBehaviour
{
    [SerializeField] private float _pushForce = 1f;
    [SerializeField] private LayerMask _pushObjectLayer;
    
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.layer == _pushObjectLayer)
        {
            var direction = (transform.position - col.transform.position).normalized;
            _rigidbody2D.AddForce(direction * _pushForce, ForceMode2D.Impulse);
        }
    }
}
