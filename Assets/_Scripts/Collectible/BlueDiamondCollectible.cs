using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Collectible;
using UnityEngine;

public class BlueDiamondCollectible : Collectible
{
    [SerializeField] private int _point;
    [SerializeField] private Animation _collect;
    [SerializeField] private AudioClip _collectSoundEffect;

    protected override void OnCollect(GameObject player)
    {
        
        Destroy(gameObject);
    }
    
}
