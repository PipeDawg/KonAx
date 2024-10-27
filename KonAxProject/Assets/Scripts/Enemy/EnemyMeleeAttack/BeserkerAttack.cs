using UnityEngine;

public class BeserkerAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GetComponentInParent<EnemyBeserker>()._canDamage)
            {
                other.GetComponentInParent<PlayerStats>().TakeDamage(GetComponentInParent<EnemyBeserker>().enemyDamage);
            }
        }
    }
}
