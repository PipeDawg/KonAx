using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float health;
    [Header("Particals")]
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Transform platicalSpawnPlace;
    [SerializeField] private Animator animator;
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    [Header("Death")]
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject restartButton;
    
    private void Start()
    {
        quitButton.SetActive(false);
        restartButton.SetActive(false);
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        Instantiate(particleSystem, platicalSpawnPlace);
        isDead();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Instantiate(particleSystem, platicalSpawnPlace);
        isDead();
    }

    private void isDead()
    {
        if (health <= 0)
        {
            animator.SetBool(IsDead, true);    
            quitButton.SetActive(true);
            restartButton.SetActive(true);
        }
    }
}