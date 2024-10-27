using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private int coinAmount;
    
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerInventory>().coins += coinAmount;
            Destroy(gameObject);
        }
    }
}