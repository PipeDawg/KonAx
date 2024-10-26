using System.Collections;
using UnityEngine;

public class MageAttack : MonoBehaviour
{
    [HideInInspector] public int Damage;
    [HideInInspector] public float bulletSpeed;
    private ParticleSystem _particleSystem;

    void Start()
    {
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
        StartCoroutine(Destroy());
        if (other.CompareTag("Player"))
        {
            GetComponent<Light>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            _particleSystem.Play();
            other.GetComponentInParent<PlayerStats>().TakeDamage(Damage);
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<MeshRenderer>().enabled = false;
        _particleSystem.Play();
    }
}
