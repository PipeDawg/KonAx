using UnityEngine;

public class BeserkerAttack : MonoBehaviour
{
    private AudioClip[] swordHitSounds;
    private void Start()
    {
        swordHitSounds = GetComponentInParent<EnemyBeserker>().attackSounds;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GetComponentInParent<EnemyBeserker>()._canDamage)
            {
                GetComponentInParent<AudioSource>().PlayOneShot(swordHitSounds[Random.Range(0, swordHitSounds.Length -1)]);
                other.GetComponentInParent<PlayerStats>().TakeDamage(GetComponentInParent<EnemyBeserker>().enemyDamage);
            }
        }
    }
}
