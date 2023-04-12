using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Script : MonoBehaviour {
    [SerializeField] private float moveSpeed;
    private float moveDir;
    private Rigidbody2D rb;

    public void Awake() {
        rb = this.GetComponent<Rigidbody2D>();
    }

    public void Update() {
        float moveDirTmp = Input.GetAxisRaw("Horizontal");
        if (moveDirTmp != 0) moveDir = moveDirTmp;
    }

    public void FixedUpdate() {
        rb.velocity = new Vector2(moveDir * moveSpeed,rb.velocity.y);
        moveDir = 0;
    }
}
