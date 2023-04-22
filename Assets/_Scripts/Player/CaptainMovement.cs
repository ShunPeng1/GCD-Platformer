using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainMovement : MonoBehaviour {
    [Header("Body part")]
    public Transform Feet;
    

    [Header("Movement Input")] 
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private float _maxSpeed = 10f, _minSpeed = 1f;
    [SerializeField] private float _pushForce = 1f;
    [SerializeField] private LayerMask _pushObjectLayerMask;
    private float _xMove, _yMove;
        
    [Header("Jump")]
    [SerializeField, Range(0.01f, 2f)] private float _jumpCooldown = 0.5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private Vector2 _groundCheckBoxSize = new Vector2(0.01f, 0.01f);
    [SerializeField] private LayerMask _groundLayerMask;
    
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

    [Header("Properties")]
    private Rigidbody2D _rigidbody2D;
    
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
        if (_xMove != 0)
        {
            _rigidbody2D.AddForce(new Vector2(_xMove * _movementSpeed, 0));
            _rigidbody2D.velocity = new Vector2( Mathf.Clamp( _rigidbody2D.velocity.x, -_maxSpeed, _maxSpeed), _rigidbody2D.velocity.y);
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(
                Mathf.Abs(_rigidbody2D.velocity.x) > _minSpeed ? _rigidbody2D.velocity.x : 0, 
                _rigidbody2D.velocity.y);   
        }

        _isFacingLeft = _xMove == 0 ? _isFacingLeft : _xMove < 0;
        _isMove =_rigidbody2D.velocity.x != 0 ;

    }

    private void Push()
    {
        
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

        //Debug.Log("Jump!");

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
        _animatorController.SetFloat(VerticalVelocity, Mathf.Clamp( Mathf.Round(_rigidbody2D.velocity.y), -1,1));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Feet.position,  _groundCheckBoxSize);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (((1 << col.gameObject.layer) & (int)_pushObjectLayerMask) != 0)
        {
            var direction = (col.transform.position - transform.position);
            col.rigidbody.AddForce(direction * _pushForce, ForceMode2D.Impulse);
        }
        
    }
    
}
