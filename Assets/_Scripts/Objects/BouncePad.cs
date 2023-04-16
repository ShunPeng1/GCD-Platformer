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
    
    [Header("Visualize")]
    private Animator _animator;

    private static readonly int IsBounce = Animator.StringToHash("IsBounce");


    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }

    /*private void FixedUpdate()
    {
        Collider2D[] results = new Collider2D[_maxBounceObject];
        var size = Physics2D.OverlapBoxNonAlloc(_hitBoxTransform.position, _hitBoxSize, 0f,  results, _objectLayerMask);
        
        if(size == 0) return;

        for (int i = 0; i < size; i++)
        {
            Bounce(results[i]);
        }
    }
    */

    /*private void Bounce(Collider2D other) {
        if (other.gameObject.CompareTag("CaptainPlayer"))
        {
            Transform feet = other.GetComponent<CaptainMovement>().Feet;
            Rigidbody2D captainRigidbody2D = other.attachedRigidbody;
            
            if (captainRigidbody2D.velocity.y < 0.1f && IsOverLap(feet.position))
            {
                captainRigidbody2D.AddForce(Vector2.up * _pushForce, ForceMode2D.Force);
                _animator.SetTrigger(IsBounce);
            }
        }
        else
        {
            Rigidbody2D otherAttachedRigidbody = other.attachedRigidbody;
            if (otherAttachedRigidbody.velocity.y < 0)
            {
                otherAttachedRigidbody.AddForce(Vector2.up * _pushForce, ForceMode2D.Force);
                _animator.SetTrigger(IsBounce);
            }
        }
    }
    */

    
    private void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.CompareTag("CaptainPlayer"))
        {
            Transform feet = other.GetComponent<CaptainMovement>().Feet;
            Rigidbody2D captainRigidbody2D = other.attachedRigidbody;
            
            if (captainRigidbody2D.velocity.y < 0.1f && _collider.OverlapPoint(feet.position))
            {
                captainRigidbody2D.AddForce(Vector2.up * _pushForce, ForceMode2D.Force);
                _animator.SetTrigger(IsBounce);
            }
        }
        else if (other.gameObject.layer == _pushLayerMask)
        {
            Rigidbody2D otherAttachedRigidbody = other.attachedRigidbody;
            if (otherAttachedRigidbody.velocity.y < 0)
            {
                otherAttachedRigidbody.AddForce(Vector2.up * _pushForce, ForceMode2D.Force);
                _animator.SetTrigger(IsBounce);
            }
        }
    }


    /*private bool IsOverLap(Vector2 position)
    {
        var hitBoxPosition = _hitBoxTransform.position;
        return position.x >= hitBoxPosition.x - _hitBoxSize.x && position.x <= hitBoxPosition.x + _hitBoxSize.x &&
               position.y >= hitBoxPosition.y - _hitBoxSize.y && position.y <= hitBoxPosition.y + _hitBoxSize.y;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(_hitBoxTransform.position, _hitBoxSize);
    }*/
}
