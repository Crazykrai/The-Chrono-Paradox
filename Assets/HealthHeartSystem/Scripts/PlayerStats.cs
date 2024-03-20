using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;
    public Animator animator;

    #region Singleton
    private static PlayerStats instance;
    public static PlayerStats Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerStats>();
            return instance;
        }
    }
    #endregion

    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxTotalHealth;

    // Invulnerability fields
    bool isInvulnerable = false;
    float invulnerabilityDuration = 0.5f; // Adjust this to change how long player has iframes 
    float invulnerabilityTimer = 0.0f;
    private bool hasTriggeredAnimation = false;

    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }
    public float MaxTotalHealth { get { return maxTotalHealth; } }

    private void FixedUpdate()
    {
        animator.SetFloat("HP", Health);
    }

    public void Heal(float health)
    {
        this.health += health;
        ClampHealth();
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvulnerable)
        {
            health -= dmg;
            ClampHealth();

            //activate iframes
            isInvulnerable = true;
            invulnerabilityTimer = invulnerabilityDuration;
        }
    }

    public void AddHealth()
    {
        if (maxHealth < maxTotalHealth)
        {
            maxHealth += 1;
            health = maxHealth;

            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }
    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }

    private void Update()
    {
        //update invulnerability timer
        if (isInvulnerable)
        {
            if (!hasTriggeredAnimation && animator.GetFloat("HP") > 0)
            {
                animator.SetTrigger("TakeDmg");
                hasTriggeredAnimation = true;
            }
            invulnerabilityTimer -= Time.deltaTime;

            //make the player vulnerable again
            if (invulnerabilityTimer <= 0.0f)
            {
                isInvulnerable = false;
                hasTriggeredAnimation = false;
            }
        }
    }
}