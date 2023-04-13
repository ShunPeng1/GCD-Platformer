using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrapMovement : MonoBehaviour
{
    [Header("Physics")] 
    [SerializeField] private Transform _feet;
    [SerializeField] private float _feetGroundCheckRadius = 1f;
    [SerializeField] private LayerMask _groundLayerMask;
    private Rigidbody2D _rigidbody2D;

    [Header("Movement")] 
    [SerializeField, Range(0.01f, 5f)] private float _movementSpeed = 1f;
    
    

    [Header("Visualize")] 
    private SpriteRenderer _spriteRenderer;
    private Animator _animatorController;
    private bool _isMove;
    private bool _isGround;
    private bool _isFacingLeft;
    private static readonly int IsMove = Animator.StringToHash("IsMove");
    private static readonly int IsGround = Animator.StringToHash("IsGround");

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
        GroundCheck();
        VisualizeAnimation();
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal"), y = Input.GetAxis("Vertical");
        _rigidbody2D.AddForce(new Vector2(x,y) * _movementSpeed, ForceMode2D.Impulse);
        
        _isFacingLeft = x == 0 ? _isFacingLeft : x < 0;
        _isMove = new Vector3(x, y, 0).magnitude > 0;
    }
    private void GroundCheck()
    {
        var hit = Physics2D.OverlapCircle(_feet.position, _feetGroundCheckRadius, _groundLayerMask);
        _isGround = hit != null;
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
