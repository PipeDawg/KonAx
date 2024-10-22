using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For the Rigidbody :D
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    private Vector3 _movementDirection;
    private bool _grounded = true;
    private Rigidbody _rb;
    
    [Header("Speeds")]
    [SerializeField] private float _movementAcceleration = 6;
    [SerializeField] private float _maxMovementSpeed = 4;
    [Space]
    [Tooltip("MainCamera of the Scene")]
    [SerializeField] GameObject Cam;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {



        if (_grounded)
        {
            if (_rb.velocity.magnitude < _maxMovementSpeed)
            {
                _movementDirection = new Vector3(Input.GetAxisRaw("Horizontal") * _movementAcceleration, 0f, Input.GetAxisRaw("Vertical") * _movementAcceleration);
                _rb.AddForce(_movementDirection);
            } else
            {
                Vector3.ClampMagnitude(_movementDirection, _maxMovementSpeed);
                //_movementDirection = new Vector3(Mathf.Clamp(Input.GetAxisRaw("Horizontal") * _movementAcceleration, -_maxMovementSpeed,_maxMovementSpeed), 0f, Mathf.Clamp(Input.GetAxisRaw("Vertical") * _movementAcceleration, -_maxMovementSpeed, _maxMovementSpeed));
                Debug.Log("TOO FASTT!!!");
            }
            
        }

        transform.rotation = Quaternion.Euler(transform.rotation.x,Cam.transform.rotation.y -90,transform.rotation.z);
        
    }
}
