using System.Collections;
using UnityEngine;

public class MageAttack : MonoBehaviour
{
    [HideInInspector] public int Damage;
    [HideInInspector] public float bulletSpeed;
    private ParticleSystem _particleSystem;

    void Start()
    {
        StartCoroutine(ProjectileExplosion(2f));
        _particleSystem = GetComponent<ParticleSystem>();
    }

    void FixedUpdate()
    {
        if (GetComponent<MeshRenderer>().enabled == true)
        {
            transform.position += transform.right * Time.deltaTime * bulletSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerStats>().TakeDamage(Damage);
            StartCoroutine(ProjectileExplosion(0));
        }
        else if (other.gameObject.name != "DetectionRange")
        {
            StartCoroutine(ProjectileExplosion(0));
        }
    }

    IEnumerator ProjectileExplosion(float time)
    {
        yield return new WaitForSeconds(time);
        Explosion();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void Explosion()
    {
        GetComponent<Light>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        _particleSystem.Play();
    }
}
