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
            Debug.Log("Reset player movement orientation to cameras forward");
        }

        Debug.Log(_rb.velocity.magnitude);
    }

     void FixedUpdate()
     {
        MoveCharacter();
     }

     void MoveCharacter() // handles the player movement through Rigidbody
     {
        if (!Physics.Raycast(transform.position, Vector3.down, 0.1f))
        {
            _rb.AddForce(new Vector3(0,gravity,0));
        }
        
        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0)
        {
            _rb.AddForce(transform.right * (movementAcceleration * Input.GetAxisRaw("Horizontal")));
            UpdatePlayerBodyOrientation(_rb.velocity);
        }
        
        if (Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0)
        {
            _rb.AddForce(transform.forward * (movementAcceleration * Input.GetAxisRaw("Vertical")));
            UpdatePlayerBodyOrientation(_rb.velocity);
        }
        
        if (_rb.velocity.magnitude > maxMovementSpeed)
        {
            _rb.velocity = _rb.velocity.normalized * maxMovementSpeed;
        }
        
        if (_rb.velocity.magnitude <= 1.1f)
        {
            _rb.velocity = Vector3.zero;
            animator.SetBool(IsMoving, false);
        }
        else  
        {
            _rb.velocity *= drag;
            animator.SetBool(IsMoving, true);
        }
     }

     void UpdatePlayerBodyOrientation(Vector3 orientation)
     {
        playerBody.transform.forward = orientation;
     }
}