using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private AudioSource _swordAudioSource;
    [SerializeField] private AudioClip[] swordClips;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
    
    [Header("Weapon")]
    public int damage;
    [SerializeField] private GameObject weapon;
    private bool _isAttacking;
    [HideInInspector] public bool canDamage;

    private void Start()
    {
        _swordAudioSource = GetComponent<AudioSource>();
        weapon.GetComponent<Collider>().enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isAttacking)
        {
            StartAttacking();
        }
    }

    private void StartAttacking()
    {    
        _swordAudioSource.PlayOneShot(swordClips[2]);
        _isAttacking = true;
        animator.SetBool(IsAttacking, true);
    }

    //Is getting called in the attack animation
    public void StopAttacking()
    {
        _isAttacking = false;
        animator.SetBool(IsAttacking, false);
    }

    //Is getting called in the attack animation
    public void CanDamage()
    {
        canDamage = true;
        weapon.GetComponent<Collider>().enabled = true;
    }

    //Is getting called in the attack animation
    public void CantDamage()
    {
        canDamage = false;
        weapon.GetComponent<Collider>().enabled = false;
    }

    public void PlaySwordSound()
    {
        _swordAudioSource.PlayOneShot(swordClips[Random.Range(0, swordClips.Length - 1)]);
    }
}