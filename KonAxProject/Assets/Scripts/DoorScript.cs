using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private static readonly int IsOpen = Animator.StringToHash("isOpen");
    [SerializeField] private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("EnemyMage") || other.CompareTag("EnemyKnight"))
        {
            animator.SetBool(IsOpen,true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("EnemyMage") || other.CompareTag("EnemyKnight"))
        {
            animator.SetBool(IsOpen, false);
        }
    }
}