using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isOpen",true);
            Debug.Log("Player entered doors trigger");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isOpen", false);
        }
    }
}
