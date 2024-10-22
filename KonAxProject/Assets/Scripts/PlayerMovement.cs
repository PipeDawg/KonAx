using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.PlayerLoop;

//For the Rigidbody :D
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    
    [Header("Speeds")]
    [SerializeField] private float _movementAcceleration = 6;
    [SerializeField] private float _maxMovementSpeed = 4;
    [Space]
    [Header("MainCamera of the Scene and player body")]
    [SerializeField] GameObject Cam;
    [SerializeField] GameObject PlayerBody;


    void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        transform.rotation = Quaternion.Euler(0,Cam.transform.rotation.eulerAngles.y -365 ,0);
        
    }

    void Update()
    {
        // Resets the players movement vector to the cameras forward face.
        if (transform.rotation != Quaternion.Euler(transform.rotation.x,Cam.transform.rotation.eulerAngles.y -365 ,transform.rotation.z))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,Cam.transform.rotation.eulerAngles.y -365 ,transform.rotation.z);
            Debug.Log("Reset player movement orientation to cameras forward");
        }
    }

     void FixedUpdate()
     {
        MoveCharacter();
     }

     void MoveCharacter() // handles the player movement through Rigidbody
     {
        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0)
        {
            _rb.AddForce(this.transform.right * _movementAcceleration * Input.GetAxisRaw("Horizontal"));
            UpdatePlayerBodyOrientation(_rb.velocity);
        }
        if (Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0)
        {
            _rb.AddForce(this.transform.forward * _movementAcceleration * Input.GetAxisRaw("Vertical"));
            UpdatePlayerBodyOrientation(_rb.velocity);
        }
        if (_rb.velocity.magnitude > -_maxMovementSpeed)
        {
            _rb.AddForce(_rb.velocity.normalized * _maxMovementSpeed);
        }
     }

     void UpdatePlayerBodyOrientation(Vector3 orientation)
     {
        PlayerBody.transform.forward = orientation;
     }
}
