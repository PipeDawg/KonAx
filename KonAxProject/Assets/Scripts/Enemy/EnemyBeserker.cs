using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBeserker : EnemyTypes
{
    void Start()
    {
        EnemySetup();
        MovementStart();
    }

    void FixedUpdate()
    {
        if (!_isDead)
        {
            Attack();
            if (!_isAttacking)
            {
                MovementHandler();
            }
        }
    }
}