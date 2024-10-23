using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private Transform _targetTransform;
    private GameObject _lastWaypoint;
    private bool _hasTarget = false;
    private bool _reverseWaypointIndex = false;
    private bool _waitingForDelay = true;
    private int _currentWaypointTargetIndex = 0;
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 0.03f;
    [SerializeField] private float rotationSpeed = 1;


    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        //_rb.useGravity = false;

        _targetTransform = waypoints[0].transform;
    }
    void Update()
    {
        if (_hasTarget)
        {
            MoveToTarget();
            TurnToTarget();
        } 
        else
        {
            PatrolCycle();
            MoveToTarget();
            if (transform.position != _targetTransform.position)
            {
                TurnToTarget();
            }
        }
    }

    void PatrolCycle()
    {
        if (transform.position == _targetTransform.position && !_waitingForDelay)
        {
            if (_currentWaypointTargetIndex < waypoints.Length -1 && !_reverseWaypointIndex)
            {
                _currentWaypointTargetIndex++;
            } else
            {
                _reverseWaypointIndex = true;
                _currentWaypointTargetIndex--;
                if (_currentWaypointTargetIndex < 0)
                {
                    _reverseWaypointIndex = false;
                    _currentWaypointTargetIndex = 0;
                }
            }
            _targetTransform = waypoints[_currentWaypointTargetIndex].transform;
            _waitingForDelay = true;
        }
        else if (transform.position == _targetTransform.position && _waitingForDelay)
        {
            StartCoroutine(WaitForPatrolDelay());
        }
    }

    IEnumerator WaitForPatrolDelay()
    {
        yield return new WaitForSeconds(3f);
        _waitingForDelay = false;
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetTransform.position, speed);
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
