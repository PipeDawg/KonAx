using UnityEngine;

//For the Rigidbody :D
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    
    [Header("Speeds")]
    [SerializeField] private float movementAcceleration = 60;
    [SerializeField] private float maxMovementSpeed = 5;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float drag = 0.9f;
    [Space]
    [Header("MainCamera of the Scene and player body")]
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject playerBody;
    [Space]
    [Header("Animation")]
    [SerializeField] private Animator animator;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _rb.useGravity = false;
        transform.rotation = Quaternion.Euler(0,cam.transform.rotation.eulerAngles.y -365 ,0);
    }

    void Update()
    {
        // Resets the players movement vector to the cameras forward face.
        if (transform.rotation != Quaternion.Euler(transform.rotation.x,cam.transform.rotation.eulerAngles.y -365 ,transform.rotation.z))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,cam.transform.rotation.eulerAngles.y -365 ,transform.rotation.z);
        }
    }

     void FixedUpdate()
     {
        MoveCharacter();
        PLayerAnimation();
     }

     private void MoveCharacter() // handles the player movement through Rigidbody
     {
        if (!Physics.Raycast(transform.position, Vector3.down, 0.1f))
        {
            _rb.AddForce(new Vector3(0,gravity,0));
        }
        
        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0)
        {
            _rb.AddForce(transform.right * (movementAcceleration * Input.GetAxisRaw("Horizontal")));
        }
        
        if (Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0)
        {
            _rb.AddForce(transform.forward * (movementAcceleration * Input.GetAxisRaw("Vertical")));
        }
        
        if (_rb.velocity.magnitude > maxMovementSpeed)
        {
            _rb.velocity = _rb.velocity.normalized * maxMovementSpeed;
        }
        
        if (_rb.velocity.magnitude <= 1.1f)
        {
            _rb.velocity = Vector3.zero;
        }
        else  
        {
            _rb.velocity *= drag;
            UpdatePlayerBodyOrientation(_rb.velocity);
        }
     }

     private void UpdatePlayerBodyOrientation(Vector3 orientation)
     {
        playerBody.transform.forward = orientation;
     }

     //Makes animation work
     private void PLayerAnimation()
     {
         if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
         {
             animator.SetBool(IsMoving, true);
         }
         else
         {
             animator.SetBool(IsMoving, false);
         }
     }

     public void StopMovement()
     {
         _rb.velocity = Vector3.zero;
     }
}