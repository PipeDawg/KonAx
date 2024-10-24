using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMage : EnemyTypes
{
    
    void Start()
    {
        MovementStart();
    }

    void Update()
    {
        MovementHandler();
    }
}
