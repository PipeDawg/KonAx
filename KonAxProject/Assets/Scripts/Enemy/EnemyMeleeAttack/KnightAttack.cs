using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GetComponentInParent<EnemyKnight>()._canDamage)
            {
                other.GetComponentInParent<PlayerStats>().TakeDamage(GetComponentInParent<EnemyKnight>().enemyDamage);
            }
        }
    }
}
