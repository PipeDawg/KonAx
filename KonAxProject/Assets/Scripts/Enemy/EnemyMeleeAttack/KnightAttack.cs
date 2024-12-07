using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    private AudioClip[] swordHitSounds;
    private void Start()
    {
        swordHitSounds = GetComponentInParent<EnemyKnight>().attackSounds;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GetComponentInParent<EnemyKnight>()._canDamage)
            {
                GetComponentInParent<AudioSource>().PlayOneShot(swordHitSounds[Random.Range(0,swordHitSounds.Length) -1]);
                other.GetComponentInParent<PlayerStats>().TakeDamage(GetComponentInParent<EnemyKnight>().enemyDamage);
            }
        }
    }
}
