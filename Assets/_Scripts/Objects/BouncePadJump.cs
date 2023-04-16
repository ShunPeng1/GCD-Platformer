using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePadJump : MonoBehaviour {
    //Constant
    const string captainFeet = "CaptainFeet";
    const string workingAnim = "Work";

    [Header("Visualize")]
    private Animator anim;

    private void Awake() {
        anim = this.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag(captainFeet)) {
            Debug.Log("Found!");
        }
    }
}
