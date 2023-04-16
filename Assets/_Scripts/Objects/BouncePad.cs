using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    private Collider2D _collider;

    [SerializeField] private float _pushForce = 10f;
    
    [Header("Visualize")]
    private Animator _animator;

    
    private void Awake() {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("CaptainPlayer"))
        {
            Transform feet = other.GetComponent<CaptainMovement>().Feet;
            Rigidbody2D captainRigidbody2D = other.attachedRigidbody;
            
            
            if (captainRigidbody2D.velocity.y < 0 && _collider.OverlapPoint(feet.transform.position))
            {
                Debug.Log("BOUNCE "+ feet.name + " "+ captainRigidbody2D.velocity);
                captainRigidbody2D.AddForce(Vector2.up * _pushForce, ForceMode2D.Force);
            }
        }
    }

    
}
