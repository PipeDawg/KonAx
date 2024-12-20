using System.Collections;
using UnityEngine;

public abstract class EnemyTypes : MonoBehaviour
{   
    private Rigidbody _rb;
    private Transform _targetTransform;
    private Vector3 _targetPositionYIgnored;
    private GameObject _lastWaypoint;
    [HideInInspector] public Animator _animator;
    private bool _hasTarget = false;
    private bool _waitingForDelay = false;
    [HideInInspector] public bool _isAttacking;
    [HideInInspector] public bool _canDamage;
    private int _currentWaypointTargetIndex = 1;
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    [HideInInspector] public bool _isDead;

    [Header("Movement Settings")]
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 0.02f;
    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private float patrolWaitTime = 0;
    [Space]
    [Header("Enemy Stats")]
    [SerializeField] private int health = 10;
    public int enemyDamage = 1;
    [SerializeField] private float attackRange = 1;
    [Header("Weapon")]
    [SerializeField] private GameObject weapon;
    [Header("ParticleSystem and SpawnPlace")]
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Transform ParticleSystemSpawnPlace;
    [Header("SoundSettings")]
    [SerializeField] public AudioClip[] attackSounds;

    public virtual void MovementStart()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        //_rb.useGravity = false;

        if (waypoints.Length > 0)
        {
            _lastWaypoint = waypoints[_currentWaypointTargetIndex];
            _targetTransform = waypoints[_currentWaypointTargetIndex].transform;
        } 
        else
        {
            print("No waypoints selected for enemy: " + gameObject.name);
        }
    }
    public virtual void MovementHandler()
    {
        if (_hasTarget)
        {
            _waitingForDelay = false;
            MoveToTarget();
            TurnToTarget();
        }
        else if (waypoints.Length > 0)
        {
            if ((transform.position - _targetPositionYIgnored).magnitude < 0.1f && !_waitingForDelay)
            {     
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
        else
        {
            print("No waypoints selected for enemy: " + gameObject.name);
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
        if (!_isAttacking)
        {
            _targetPositionYIgnored = new Vector3(_targetTransform.position.x, transform.position.y, _targetTransform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, _targetPositionYIgnored, speed);
            if (_rb.velocity.magnitude > speed)
            {
                _rb.velocity = _rb.velocity.normalized * speed;
            }
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
                _isAttacking = false;
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
        Instantiate(particleSystem, ParticleSystemSpawnPlace);
        if (health <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        _isDead = true;
        _animator.SetBool(IsDead, true);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public virtual void EnemySetup()
    {
        _animator = GetComponent<Animator>();
        weapon.GetComponent<Collider>().enabled = false;
    }

    public virtual void Attack()
    {
        if (_hasTarget && Mathf.Abs((transform.position - _targetTransform.position).magnitude) <= attackRange)
        {
            AttackSound();
            _isAttacking = true;
            _animator.SetBool(IsAttacking, true);
        }
    }

    public void StopAttacking()
    {
        _isAttacking = false;
        _animator.SetBool(IsAttacking, false);
    }

    public virtual void CanDamage()
    {
        _canDamage = true;
        weapon.GetComponent<Collider>().enabled = true;
    }

    public virtual void CantDamage()
    {
        _canDamage = false;
        weapon.GetComponent<Collider>().enabled = false;
    }

    public virtual void AttackSound()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().clip = attackSounds[2];
            GetComponent<AudioSource>().Play();
        }
    }
}