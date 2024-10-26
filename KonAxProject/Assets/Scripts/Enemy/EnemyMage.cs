using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMage : EnemyTypes
{
    private List<GameObject> projectiles = new List<GameObject>();
    [Header("Projectile spawnpoint and Prefab")]
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float _projectileSpeed;

    void Start()
    {
        _animator = GetComponent<Animator>();
        MovementStart();
    }

    private void FixedUpdate()
    {
        if (!_isDead)
        {
            Attack();
            MovementHandler();
        }
    }

    public override void Attack()
    {
        base.Attack();
    }
    
    public void Shoot()
    {
        var projectile = Instantiate(projectilePrefab,projectileSpawn.position, projectileSpawn.rotation);
        projectile.GetComponent<MageAttack>().bulletSpeed = _projectileSpeed;
        projectile.GetComponent<MageAttack>().Damage = enemyDamage;
        projectiles.Add(projectile);
    }
}

