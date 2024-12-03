using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private Animator imageAnimator;
    private static readonly int FadeOut = Animator.StringToHash("FadeOut");
    private static readonly int FadeIn = Animator.StringToHash("FadeIn");
    [Space]
    [SerializeField] private GameObject canvasImage;
    
    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        canvasImage.SetActive(true);
        imageAnimator.SetBool(FadeIn, false);
        imageAnimator.SetBool(FadeOut, true);
    }
}