using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyKnight : EnemyTypes
{
    void Start()
    {
        EnemySetup();
        MovementStart();
    }

    private void FixedUpdate()
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