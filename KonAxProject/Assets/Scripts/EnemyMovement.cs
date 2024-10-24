using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private Transform _targetTransform;
    private Vector3 _targetPositionYIgnored;
    private GameObject _lastWaypoint;
    private bool _hasTarget = false;
    private bool _reverseWaypointIndex = false;
    private bool _waitingForDelay = false;
    private bool _isMoving = false;
    private int _currentWaypointTargetIndex = 1;

    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 0.03f;
    [SerializeField] private float rotationSpeed = 1;


    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        //_rb.useGravity = false;
    
        _targetTransform = waypoints[_currentWaypointTargetIndex].transform;
    }
    void Update()
    {
        if (_hasTarget)
        {
            _waitingForDelay = false;
            MoveToTarget();
            TurnToTarget();
        } 
        else
        {
            if (transform.position == _targetTransform.position)
            {     

                PatrolCycle();
                if (_waitingForDelay)
                {
                    MoveToTarget();
                    WaitForPatrolDelay();
                    Debug.Log("run Timer");
                }
                
            } else if (transform.position != _targetPositionYIgnored && !_waitingForDelay)
            {

                MoveToTarget();
                TurnToTarget();
            }
        }
    }

    void PatrolCycle()
    {
        if (_currentWaypointTargetIndex > waypoints.Length - 1)
        {
            _currentWaypointTargetIndex = 0;
        }
        else
        {
            _currentWaypointTargetIndex++;
        }
        _lastWaypoint = waypoints[_currentWaypointTargetIndex];
        _targetTransform = waypoints[_currentWaypointTargetIndex].transform;
    }

    IEnumerator WaitForPatrolDelay()
    {
        yield return new WaitForSeconds(3f);
        _waitingForDelay = false;
    }

    void MoveToTarget()
    {
        _targetPositionYIgnored = new Vector3(_targetTransform.position.x, transform.position.y, _targetTransform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, _targetPositionYIgnored, speed);
    }

    void TurnToTarget()
    {
        Vector3 direction = _targetTransform.position - transform.position;
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        targetRotation.x = 0;
        targetRotation.z = 0;
        transform.rotation = targetRotation;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _targetTransform = other.transform;
            _hasTarget = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (waypoints.Length > 0)
            {
                _hasTarget = false;
                _targetTransform = _lastWaypoint.transform;
            } 
            else
            {
             Debug.Log("No waypoints set to array");
            }
        }
    }


}
