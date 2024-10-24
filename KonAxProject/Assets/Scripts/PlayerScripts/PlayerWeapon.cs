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
    }
}