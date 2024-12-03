using UnityEngine;
using UnityEngine.SceneManagement;
public class ImageHandler : MonoBehaviour
{
    public void SetImageInactive()
    {
        gameObject.SetActive(false);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}