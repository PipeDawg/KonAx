using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int health;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
