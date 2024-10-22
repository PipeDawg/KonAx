using Unity.VisualScripting;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField] private int health; //How many hits it takes before the object is destroyed
    [Space]
    [SerializeField] private Animator animator;
    
    //!NOTE for this to work, then the animation parameter name has to be "OnDeath"
    private static readonly int Death = Animator.StringToHash("OnDeath");

    //Call this function, when this object is being hit
    public void OnHit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDeath();    
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnHit(1);
        }
    }
}