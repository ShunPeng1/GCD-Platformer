using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PressButton<T> : MonoBehaviour where T : Enum
{
    [SerializeField] protected LayerMask _activeLayer;

    public bool IsPressing = false;

    protected Animator _animator;
    protected int _count;
    protected static readonly int Pressing = Animator.StringToHash("IsPressing");
    

    protected void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if  (((1 << col.gameObject.layer) & _activeLayer) != 0)
        {
            _count++;
            if (_count > 0)
            {
                IsPressing = true;
                _animator.SetBool(Pressing, IsPressing);
                Active();
            }

        }
    }
    
    
    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        if  (((1 << col.gameObject.layer) & _activeLayer) != 0)
        {
            _count--;
            if (_count == 0)
            {
                IsPressing = false;
                _animator.SetBool(Pressing, IsPressing);
                Inactive();
            }
        }
    }

    protected abstract void Active();
    protected abstract void Inactive();

}
