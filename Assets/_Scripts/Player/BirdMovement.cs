using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    [Header("Physics")] [SerializeField] private Transform _feet;
    [SerializeField] private float _feetGroundCheckRadius = 1f;
    [SerializeField] private LayerMask _groundLayerMask;
    private Rigidbody2D _rigidbody2D;

    [Header("Mouse Input")] [SerializeField] private float _mouseSensitivity = 1f;
    private Vector3 _mousePosition;
    private float _moveX, _moveY;
    
    [Header("Visualize")] private SpriteRenderer _spriteRenderer;
    private Animator _animatorController;
    private bool _isMove;
    private bool _isGround;
    private bool _isFacingLeft;
    private bool _isAlive = true;
    private static readonly int IsMove = Animator.StringToHash("IsMove");
    private static readonly int IsGround = Animator.StringToHash("IsGround");
    private static readonly int IsAlive = Animator.StringToHash("IsAlive");


    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animatorController = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start() {
        GroundCheck();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isAlive) return;
        GetInput();
        GroundCheck();
        VisualizeAnimation();
    }

    void FixedUpdate()
    {
        if (!_isAlive) return;
        Movement();
    }

    private void GetInput()
    {
        _moveX = Input.GetAxisRaw("Mouse X");
        _moveY = Input.GetAxisRaw("Mouse Y");
    }


    private void Movement()
    {
        _rigidbody2D.velocity = new Vector2(_moveX, _moveY) * _mouseSensitivity;
        _isFacingLeft = _moveX == 0 ? _isFacingLeft : _moveX < 0;
        _isMove = new Vector3(_moveX, _moveY, 0).magnitude > 0;
        _moveX = _moveY = 0;
    }

    private void GroundCheck()
    {
        var hit = Physics2D.OverlapCircle(_feet.position, _feetGroundCheckRadius, _groundLayerMask);
        _isGround = hit != null;
    }

    public void Kill()
    {
        _isAlive = false;
        _animatorController.SetBool(IsAlive, false);
        _rigidbody2D.velocity = Vector2.zero;
        
    }

    public void Dead()
    {
        
    }

    private void VisualizeAnimation()
    {
        _spriteRenderer.flipX = _isFacingLeft;
        _animatorController.SetBool(IsGround, _isGround);
        _animatorController.SetBool(IsMove, _isMove);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_feet.position, _feetGroundCheckRadius);
    }
}