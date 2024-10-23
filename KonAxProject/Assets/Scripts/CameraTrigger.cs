using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraMover.instance.MoveCamera(cameraPosition);
        }
    }
}
