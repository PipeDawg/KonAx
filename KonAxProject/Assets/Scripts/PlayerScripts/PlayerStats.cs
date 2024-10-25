using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int health;
    [Header("Particals")]
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Transform platicalSpawnPlace;
    public void TakeDamage(int damage)
    {
        health -= damage;
        Instantiate(particleSystem, platicalSpawnPlace);
    }
}
