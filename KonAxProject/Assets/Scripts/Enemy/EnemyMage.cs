using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMage : EnemyTypes
{
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;

    void Start()
    {
        EnemySetup();
        MovementStart();
    }

    private void FixedUpdate()
    {
        if (!_isDead)
        {
            Attack();
            MovementHandler();
            if (_isAttacking)
            {
                Shoot();
            }
        }
    }

    IEnumerable Shoot()
    {
        yield return new WaitForSeconds(1.5f);
        var projectile = Instantiate(projectilePrefab,projectileSpawn.position, projectileSpawn.rotation);
        projectile.AddComponent<MageAttack>().Damage = enemyDamage;
        //projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * projectileSpeed);
        weapon = projectile;
    }
}

