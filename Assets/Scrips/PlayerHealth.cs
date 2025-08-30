using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    private Animator animator;
    private PlayerMovement movement;
    private PlayerShooter shooter;
    private UiManager uiManager;

    public Slider slider;

    private AudioSource audioSource;
    public AudioClip damageClip;
    public AudioClip deathClip;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        shooter = GetComponent<PlayerShooter>();
        audioSource = GetComponent<AudioSource>();

        var findGo = GameObject.FindWithTag("UIManager");
        uiManager = findGo.GetComponent<UiManager>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        movement.enabled = true;
        shooter.enabled = true;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if(IsDead) return;

        base.OnDamage(damage, hitPoint, hitNormal);

        UpdateHPSlider(Health, MaxHealth);
        audioSource.PlayOneShot(damageClip);
    }

    protected override void Die()
    {
        base.Die();

        animator.SetTrigger("Death");
        audioSource.PlayOneShot(deathClip);

        movement.enabled = false;
        shooter.enabled = false;
    }

    public void UpdateHPSlider(float currentHp, float maxHp)
    {
        slider.value = currentHp / maxHp;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
