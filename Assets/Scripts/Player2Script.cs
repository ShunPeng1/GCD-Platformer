using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Script : MonoBehaviour {
    private Vector3 mousePos;
    private Rigidbody2D rb;

    public void Awake() {
        rb = this.GetComponent<Rigidbody2D>();
    }
    
    public void Update() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void FixedUpdate() {
        Vector3 movePos = Vector3.Lerp(transform.position,mousePos,0.3f);
        rb.MovePosition(movePos);
    }
}
