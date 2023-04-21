using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PressButton : MonoBehaviour
{
    [SerializeField] private LayerMask _activeLayer;
    public bool IsPress = false;

    private Animator _animator;
    private int _count;
    private static readonly int Press = Animator.StringToHash("Press");

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
                IsPress = true;
                _animator.SetFloat(Press, 1f);
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
                IsPress = false;
                _animator.SetFloat(Press, 0f);
            }
        }
    }
}
