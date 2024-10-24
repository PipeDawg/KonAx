using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int health; //How many hits it takes before the object is destroyed
    [Header("ItemDrops")]
    [SerializeField] private List<GameObject> itemDrops = new List<GameObject>();
    [SerializeField] private Transform dropPlace;
    [Header("Animation")]
    [SerializeField] private Animator animator;
    
    private static readonly int Death = Animator.StringToHash("OnDeath");
    private static readonly int Hit = Animator.StringToHash("OnHit");

    //Call this function, when this object is being hit
    public void OnHit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDeath();    
        }
        else
        {
            animator.SetBool(Hit, true); //Starts the "taking damage" animation
        }
    }

    private void OnDeath()
    {
        animator.SetBool(Death, true); //Starts the death animation
        Drop();
    }

    private void Drop()
    {
        int number = Random.Range(0, itemDrops.Count);
        if (itemDrops[number] != null)
        {
            Instantiate(itemDrops[number], dropPlace.position, Quaternion.identity);
        }
    }

    //Gets called in an animation event
    public void DestroyAfterTime()
    {
        Destroy(this.GameObject());
    }

    // Gets called in an animation event 
    public void StopHitAnimation()
    {
        animator.SetBool(Hit, false);
    }
}