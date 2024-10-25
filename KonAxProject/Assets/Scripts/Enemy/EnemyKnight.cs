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

    void Update()
    {
        Attack();
        if (!_isAttacking)
        {
            MovementHandler();
        }
    }
}
