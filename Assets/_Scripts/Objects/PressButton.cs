using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PressButton : MonoBehaviour
{
    [SerializeField] private LayerMask _activeLayer;
    public bool IsPressing = false;

    private Animator _animator;
    private int _count;
    private static readonly int Pressing = Animator.StringToHash("IsPressing");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if  (((1 << col.gameObject.layer) & _activeLayer) != 0)
        {
            _count++;
            if (_count > 0)
            {
                IsPressing = true;
                _animator.SetBool(Pressing, IsPressing);
            }

        }
    }
    
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if  (((1 << col.gameObject.layer) & _activeLayer) != 0)
        {
            _count--;
            if (_count == 0)
            {
                IsPressing = false;
                _animator.SetBool(Pressing, IsPressing);
            }
        }
    }
}
