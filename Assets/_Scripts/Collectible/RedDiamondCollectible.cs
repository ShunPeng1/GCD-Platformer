using System.Collections;
using System.Collections.Generic;
using _Scripts.Collectible;
using UnityEngine;

public class RedDiamondCollectible : Collectible {
    const string collectAnim = "Collect";

    [Header("Visiualizational")]
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip _collectSoundEffect;

    [Header("Info")]
    [SerializeField] private int _point;

    protected override void OnCollect(GameObject player) {
        _animator.Play(collectAnim);
    }
}
