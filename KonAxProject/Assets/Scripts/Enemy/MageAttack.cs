using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MageAttack : MonoBehaviour
{
    public int Damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GetComponentInParent<EnemyKnight>()._canDamage)
            {
                other.GetComponentInParent<PlayerStats>().TakeDamage(Damage);
            }
        }
        Destroy(gameObject);
    }
}
