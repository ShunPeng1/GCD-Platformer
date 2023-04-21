using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Interactive_Objects;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

[RequireComponent(typeof(Rigidbody2D))]
public class ActivationBar : MonoBehaviour
{
    public ActivationColor BarActivationColor;
    [SerializeField] private Vector3 _inactivePosition, _activePosition;
    [SerializeField] private float _movingDuration=  1f;
    [SerializeField] private Ease _ease = Ease.OutCubic;
    private Rigidbody2D _rigidbody2D;
    private Sequence _sequence;
    private Vector3 _destinationPosition;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _destinationPosition = transform.position;
    }
    

    public void ActiveMove()
    {
        _sequence.Kill();
        _sequence = DOTween.Sequence();

        _destinationPosition += _activePosition;
        _sequence.Append( 
            _rigidbody2D.DOMove(_destinationPosition, _movingDuration).SetEase(_ease)
            );
        
        _sequence.Play();
    }


    public void InactiveMove()
    {
        _sequence.Kill();
        _sequence = DOTween.Sequence();
        
        _destinationPosition += _inactivePosition;
        _sequence.Append( 
            _rigidbody2D.DOMove(_destinationPosition, _movingDuration).SetEase(_ease)
        );
        _sequence.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(_inactivePosition + transform.position, "Inactive Position", false, Color.red);
        Gizmos.DrawIcon(_activePosition + transform.position, "Active Position", false, Color.green);
    }
    
}
