using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float health;
    [Header("Particals")]
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Transform platicalSpawnPlace;
    [SerializeField] private Animator animator;
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    [Header("Death")]
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject restartButton;

    /// <summary>
    ///             Healthbar below
    /// </summary>
    [Header("Healthbar")]
    public Image healthBar;
    float maxHelath;
    [SerializeField] float lerpSpeed;

    private void Start()
    {
        quitButton.SetActive(false);
        restartButton.SetActive(false);
        maxHelath = health;
    }

    private void Update()
    {
        lerpSpeed = 3f * Time.deltaTime;
        HealthBarFiller();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        Instantiate(particleSystem, platicalSpawnPlace);
        isDead();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        HealthBarFiller();
        Instantiate(particleSystem, platicalSpawnPlace);
        isDead();
    }

    private void isDead()
    {
        if (health <= 0)
        {
            animator.SetBool(IsDead, true);    
            SetButtonsActive();
            GetComponent<PlayerMovement>().StopMovement();
            GetComponent<PlayerMovement>().enabled = false;
        }
    }
    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHelath, lerpSpeed);
    }

    public void SetButtonsActive()
    {
        quitButton.SetActive(true);
        restartButton.SetActive(true);
    }
    
    private void Destroy()
    {
        //This function is just to prevent an error
    }
}