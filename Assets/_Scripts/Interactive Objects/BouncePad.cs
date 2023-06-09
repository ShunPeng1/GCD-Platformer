using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{

    /*[Header("Trigger Box")]
    [SerializeField] private Transform _hitBoxTransform;
    [SerializeField] private Vector2 _hitBoxSize = new Vector2(1,1);
    [SerializeField] private float _pushForce = 10f;
    [SerializeField] private int _maxBounceObject = 3;
    */
    
    [Header("Trigger Box")]
    [SerializeField] private float _pushForce = 1500;
    [SerializeField] private float _minDropSpeed = 1f;
    [SerializeField] private LayerMask _pushLayerMask;
    private Collider2D _collider;
    private Vector2 _pushDirection;
    
    [Header("Visualize")]
    private Animator _animator;

    private static readonly int IsBounce = Animator.StringToHash("IsBounce");


    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();

        var z = transform.eulerAngles.z;
        _pushDirection = new Vector2(- Mathf.Sin(z * Mathf.Deg2Rad), Mathf.Cos(z * Mathf.Deg2Rad));
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.CompareTag("CaptainPlayer"))
        {
            Transform feet = other.GetComponent<CaptainMovement>().Feet;
            Rigidbody2D captainRigidbody2D = other.attachedRigidbody;
            
            if (captainRigidbody2D.velocity.y < _minDropSpeed && _collider.OverlapPoint(feet.position))
            {
                captainRigidbody2D.velocity = new Vector2(0, 0);
                captainRigidbody2D.AddForce(_pushDirection * _pushForce, ForceMode2D.Force);
                _animator.SetTrigger(IsBounce);
            }
        }
        else if (other.gameObject.layer == _pushLayerMask)
        {
            Rigidbody2D otherAttachedRigidbody = other.attachedRigidbody;
            if (otherAttachedRigidbody.velocity.y < _minDropSpeed)
            {
                otherAttachedRigidbody.velocity = new Vector2(0, 0);
                otherAttachedRigidbody.AddForce(_pushDirection * _pushForce, ForceMode2D.Force);
                _animator.SetTrigger(IsBounce);
            }
        }
    }

    
    private void OnTriggerStay2D (Collider2D other) {
        if (other.gameObject.CompareTag("CaptainPlayer"))
        {
            Transform feet = other.GetComponent<CaptainMovement>().Feet;
            Rigidbody2D captainRigidbody2D = other.attachedRigidbody;
            
            if (captainRigidbody2D.velocity.y < _minDropSpeed && _collider.OverlapPoint(feet.position))
            {
                captainRigidbody2D.velocity = new Vector2(0, 0);
                captainRigidbody2D.AddForce(_pushDirection * _pushForce, ForceMode2D.Force);
                _animator.SetTrigger(IsBounce);
            }
        }
        else if  (((1 << other.gameObject.layer) & _pushLayerMask) != 0)
        {
            Rigidbody2D otherAttachedRigidbody = other.attachedRigidbody;
            if (otherAttachedRigidbody.velocity.y < _minDropSpeed)
            {
                otherAttachedRigidbody.velocity = new Vector2(0, 0);
                otherAttachedRigidbody.AddForce(_pushDirection * _pushForce, ForceMode2D.Force);
                _animator.SetTrigger(IsBounce);
            }
        }
    }

}
