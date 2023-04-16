using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Collectible
{
    public enum CollectibleType
    {
        Coins,
        BlueDiamond,
        RedDiamond,
        Health
    }

    [RequireComponent(typeof(Collider2D))]
    public abstract class Collectible : MonoBehaviour
    {
        [SerializeField] private string _collectorTag;
        
        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag(_collectorTag))
            {
                OnCollect(col.gameObject);
            } 
        }

        protected abstract void OnCollect(GameObject player);

        public virtual void DestroyCollectible()
        {
            Destroy(gameObject);
        }
    }
}