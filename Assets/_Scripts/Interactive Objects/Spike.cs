using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
    
        if (col.gameObject.CompareTag("CaptainPlayer"))
        {
            col.gameObject.GetComponent<CaptainMovement>().Kill();
        }
    
        if (col.gameObject.CompareTag("BirdPlayer"))
        {
            col.gameObject.GetComponent<BirdMovement>().Kill();
        }
        
    }
}
