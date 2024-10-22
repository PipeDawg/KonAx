using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.PlayerLoop;

//For the Rigidbody :D
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    
    [Header("Speeds")]
    [SerializeField] private float movementAcceleration = 6;
    [SerializeField] private float maxMovementSpeed = 4;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float drag = 0.9f;
    [Space]
    [Header("MainCamera of the Scene and player body")]
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject playerBody;


    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        transform.rotation = Quaternion.Euler(0,cam.transform.rotation.eulerAngles.y -365 ,0);
        
    }

    void Update()
    {
        // Resets the players movement vector to the cameras forward face.
        if (transform.rotation != Quaternion.Euler(transform.rotation.x,cam.transform.rotation.eulerAngles.y -365 ,transform.rotation.z))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,cam.transform.rotation.eulerAngles.y -365 ,transform.rotation.z);
            Debug.Log("Reset player movement orientation to cameras forward");
        }

        Debug.Log(rb.velocity.magnitude);
    }

     void FixedUpdate()
     {
        MoveCharacter();
     }

     void MoveCharacter() // handles the player movement through Rigidbody
     {
        if (!Physics.Raycast(transform.position, Vector3.down, 0.1f))
        {
            rb.AddForce(new Vector3(0,gravity,0));
        }
        
        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0)
        {
            rb.AddForce(this.transform.right * movementAcceleration * Input.GetAxisRaw("Horizontal"));
            UpdatePlayerBodyOrientation(rb.velocity);
        }
        if (Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0)
        {
            rb.AddForce(this.transform.forward * movementAcceleration * Input.GetAxisRaw("Vertical"));
            UpdatePlayerBodyOrientation(rb.velocity);
        }
        if (rb.velocity.magnitude > maxMovementSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxMovementSpeed;
        }
        if (rb.velocity.magnitude <= 0.1f)
        {
            rb.velocity = Vector3.zero;
        } else  
        {
            rb.velocity *= drag;
        }
     }

     void UpdatePlayerBodyOrientation(Vector3 orientation)
     {
        playerBody.transform.forward = orientation;
     }
}
