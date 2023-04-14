using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainMovement : MonoBehaviour
{
    [Header("Physics")] 
    [SerializeField] private Transform _feet;
    [SerializeField] private float _feetGroundCheckRadius = 1f;
    [SerializeField] private LayerMask _groundLayerMask;
    private Rigidbody2D _rigidbody2D;

    [Header("Movement")] 
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _gravityForce = 1f;
    
    [Header("Visualize")] 
    private SpriteRenderer _spriteRenderer;
    private Animator _animatorController;
    private bool _isMove;
    private bool _isGround;
    private bool _isFacingLeft;
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
        Movement();
        Jump();
        GroundCheck();
        VisualizeAnimation();
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        _rigidbody2D.velocity =  new Vector2(x * _movementSpeed, _rigidbody2D.velocity.y);
        
        _isFacingLeft = x == 0 ? _isFacingLeft : x < 0;
        _isMove = new Vector3(x, 0, 0).magnitude > 0;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_isGround)
            {
                
                Debug.Log("Jump!");
                _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            }
        }
    }
    
    private void GroundCheck()
    {

        var hit = Physics2D.OverlapCircle(_feet.position, _feetGroundCheckRadius, _groundLayerMask);
        _isGround = hit != null;
        //Debug.Log("Hit Ground");
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
        Gizmos.DrawWireSphere(_feet.position, _feetGroundCheckRadius);
    }
}
