using UnityEngine;
using TMPro;

public class PlayerFinish : MonoBehaviour
{
    [SerializeField] private Transform startTransform;
    [Header("FinishText")]
    [SerializeField] private TextMeshProUGUI finishText; //The text
    [SerializeField] private GameObject finishTextObject; //The object with the finish text

    private PlayerInventory _playerInventory;

    private void Start()
    {
        _playerInventory = GetComponent<PlayerInventory>();
        finishTextObject.SetActive(true);
        finishText.text = "You have found the throne\nYou found " + _playerInventory.coins + " coins on your way";
    }

    void Update()
    {
        transform.rotation *= Quaternion.FromToRotation(startTransform.forward, Vector3.right);
    }
}