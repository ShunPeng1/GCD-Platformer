using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainMovement : MonoBehaviour {
    [Header("Physics")] 
    public Transform Feet;
    [SerializeField] private Vector2 _groundCheckBoxSize = new Vector2(0.01f, 0.01f);
    [SerializeField] private LayerMask _groundLayerMask;
    private Rigidbody2D _rigidbody2D;

    [Header("Movement Input")] 
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _fallGravityForce = 1f;
    [SerializeField, Range(0.01f, 2f)] private float _jumpCooldown = 0.5f;
    private float _xMove, _yMove;
        
    [Header("Visualize")] 
    private SpriteRenderer _spriteRenderer;
    private Animator _animatorController;
    private bool _isMove;
    private bool _isGround;
    private bool _isFacingLeft;
    private bool _isJumping;
    private static readonly int IsMove = Animator.StringToHash("IsMove");
    private static readonly int IsGround = Animator.StringToHash("IsGround");
    private static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animatorController = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Jump();
        GroundCheck();
        VisualizeAnimation();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void GetInput()
    {
        _xMove = Input.GetAxis("Horizontal");
    }

    private void Movement()
    {
        _rigidbody2D.velocity =  new Vector2(_xMove * _movementSpeed, _rigidbody2D.velocity.y);
        _isFacingLeft = _xMove == 0 ? _isFacingLeft : _xMove < 0;
        _isMove = _xMove > 0;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_isGround)
            {
                StartCoroutine(JumpCooldown());
            }
        }
    }

    private IEnumerator JumpCooldown()
    {
        if (_isJumping || !_isGround) yield break;

        Debug.Log("Jump!");

        _isJumping = true;
        _isGround = false;
        _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        
        yield return new WaitForSeconds(_jumpCooldown);
        _isJumping = false;
    }
    
    private void GroundCheck() {
        if (_isJumping) return;
        var hit = Physics2D.OverlapBox(Feet.position, _groundCheckBoxSize, 0, _groundLayerMask);
        //var hit = Physics2D.OverlapCircle(_feet.position, _feetGroundCheckRadius, _groundLayerMask);
        _isGround = hit != null;
    }

    private void VisualizeAnimation()
    {
        _spriteRenderer.flipX = _isFacingLeft;
        _animatorController.SetBool(IsGround, _isGround);
        _animatorController.SetBool(IsMove, _isMove);
        _animatorController.SetFloat(VerticalVelocity, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Feet.position, _groundCheckBoxSize);
    }
}
