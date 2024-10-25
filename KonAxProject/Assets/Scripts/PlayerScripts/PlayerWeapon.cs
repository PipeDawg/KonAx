using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DestructibleObject"))
        {
            if (GetComponentInParent<PlayerAttack>().canDamage)
            {
                other.GetComponent<DestructibleObject>().OnHit(GetComponentInParent<PlayerAttack>().damage);
            }
        } 
        else if (other.CompareTag("EnemyKnight"))
        {
            if (GetComponentInParent<PlayerAttack>().canDamage)
            {
                other.GetComponentInParent<EnemyKnight>().OnHit(GetComponentInParent<PlayerAttack>().damage);
            }
        }
        else if (other.CompareTag("EnemyMage"))
        {
            if (GetComponentInParent<PlayerAttack>().canDamage)
            {
                other.GetComponent<EnemyMage>().OnHit(GetComponentInParent<PlayerAttack>().damage);
            }
        }
    }
}