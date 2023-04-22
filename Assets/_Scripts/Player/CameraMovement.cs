using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private BirdMovement _birdMovement;
    [SerializeField] private CaptainMovement _captainMovement;
    
    void Start()
    {
        
    }
    
    void LateUpdate()
    {
        transform.position = new Vector3(_captainMovement.transform.position.x , transform.position.y, transform.position.z);
    }
}
