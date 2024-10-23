using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraMover.Instance.MoveCamera(cameraPosition);
        }
    }
}