using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{

    [Header("Trigger Box")]
    [SerializeField] private Transform _hitBoxTransform;
    [SerializeField] private Vector2 _hitBoxSize = new Vector2(1,1);
    [SerializeField] private float _pushForce = 10f;
    [SerializeField] private LayerMask _objectLayerMask;
    [SerializeField] private float _minDropSpeed = 1f;
    
    [Header("Visualize")]
    private Animator _animator;

    private static readonly int IsBounce = Animator.StringToHash("IsBounce");


    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        var hits = Physics2D.OverlapBoxAll(_hitBoxTransform.position, _hitBoxSize, 0f, _objectLayerMask);
        
        if(hits == null) return;
        
        foreach (var hit in hits)
        {
            Bounce(hit);
        }
    }

    private void Bounce(Collider2D other) {
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
            Rigidbody2D rigidbody2D = other.attachedRigidbody;
            if (rigidbody2D.velocity.y < 0)
            {
                rigidbody2D.AddForce(Vector2.up * _pushForce, ForceMode2D.Force);
                _animator.SetTrigger(IsBounce);
            }
        }
    }


    private bool IsOverLap(Vector2 position)
    {
        var hitBoxPosition = _hitBoxTransform.position;
        

        return position.x >= hitBoxPosition.x - _hitBoxSize.x && position.x <= hitBoxPosition.x + _hitBoxSize.x &&
               position.y >= hitBoxPosition.y - _hitBoxSize.y && position.y <= hitBoxPosition.y + _hitBoxSize.y;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(_hitBoxTransform.position, _hitBoxSize);
    }
}
