using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private void Start()
    {
        playerAttack = GetComponentInParent<PlayerAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DestructibleObject"))
        {
            if (GetComponentInParent<PlayerAttack>().canDamage)
            {
                other.GetComponent<DestructibleObject>().OnHit(GetComponentInParent<PlayerAttack>().damage);
                playerAttack.PlaySwordSound();
            }
        } 
        else if (other.CompareTag("EnemyKnight"))
        {
            if (GetComponentInParent<PlayerAttack>().canDamage)
            {
                other.GetComponentInParent<EnemyKnight>().OnHit(GetComponentInParent<PlayerAttack>().damage);
                playerAttack.PlaySwordSound();
            }
        }
        else if (other.CompareTag("EnemyMage"))
        {
            if (GetComponentInParent<PlayerAttack>().canDamage)
            {
                other.GetComponentInParent<EnemyMage>().OnHit(GetComponentInParent<PlayerAttack>().damage);
                playerAttack.PlaySwordSound();
            }
        }
        else if (other.CompareTag("EnemyBeserker"))
        {
            if (GetComponentInParent<PlayerAttack>().canDamage)
            {
                other.GetComponentInParent<EnemyBeserker>().OnHit(GetComponentInParent<PlayerAttack>().damage);
                playerAttack.PlaySwordSound();
            }
        }
        
    }
}