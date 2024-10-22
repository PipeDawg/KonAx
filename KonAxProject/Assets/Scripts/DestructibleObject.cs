using Unity.VisualScripting;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField] private int health; //How many hits it takes before the object is destroyed
    [Space]
    [SerializeField] private Animator animator;
    
    //!NOTE for this to work, then the animation parameter name has to be "OnDeath"
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnHit(1);
        }
    }
}