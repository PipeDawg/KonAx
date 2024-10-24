using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyTypes : MonoBehaviour
{   
    private Rigidbody _rb;
    private Transform _targetTransform;
    private Vector3 _targetPositionYIgnored;
    private GameObject _lastWaypoint;
    private bool _hasTarget = false;
    private bool _waitingForDelay = false;
    private int _currentWaypointTargetIndex = 1;

    [Header("Movement Settings")]
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 0.02f;
    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private float patrolWaitTime = 0;
    [Space]
    [Header("Enemy Stats")]
    [SerializeField] private int health = 10;
    [SerializeField] private int enemyDamage = 1;

    public virtual void MovementStart()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        //_rb.useGravity = false;
    
        _lastWaypoint = waypoints[_currentWaypointTargetIndex];
        _targetTransform = waypoints[_currentWaypointTargetIndex].transform;
    }
    public virtual void MovementHandler()
    {
        if (_hasTarget)
        {
            _waitingForDelay = false;
            MoveToTarget();
            TurnToTarget();
        }
        else
        {
            if (transform.position == _targetPositionYIgnored && !_waitingForDelay)
            {     
                Debug.Log("no more targeting player");
                _waitingForDelay = true;
                PatrolCycle();
                if (_waitingForDelay)
                {
                    MoveToTarget();
                    StartCoroutine(WaitForPatrolDelay());
                }
            } 
            else if (transform.position != _targetPositionYIgnored && !_waitingForDelay)
            {
                MoveToTarget();
                TurnToTarget();
            }
        }
    }

    void PatrolCycle()
    {
        if (_currentWaypointTargetIndex >= waypoints.Length - 1)
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
        
        yield return new WaitForSeconds(patrolWaitTime);
        _waitingForDelay = false;
    }

    void MoveToTarget()
    {
        _targetPositionYIgnored = new Vector3(_targetTransform.position.x, 0, _targetTransform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, _targetPositionYIgnored, speed);
        if (_rb.velocity.magnitude > speed)
        {
            _rb.velocity = _rb.velocity.normalized * speed;
        }
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

    public void OnHit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }
}