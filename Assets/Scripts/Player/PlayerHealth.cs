using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    PlayerStats playerStats;

    public int currentHealth;
    public int newHealth;
    public int maxHealth;
    private float lerpTimer;

    [Header("Health Bar")]
    [SerializeField]
    private float chipSpeed = 2f;
    [SerializeField]
    private Image frontHealthBar;
    [SerializeField]
    private Image backHealthBar;

    [Header("Damage Overlay")]
    [SerializeField]
    private Image overlay;
    [SerializeField]
    private float duration;
    [SerializeField]
    private float fadeSpeed;

    private float durationTimer;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();

        currentHealth = playerStats.currentHealth;
        newHealth = currentHealth;
        maxHealth = playerStats.maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    private void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
        if (overlay.color.a > 0)
        {
            if (currentHealth < 30)
                return;

            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                float tempAplha = overlay.color.a;
                tempAplha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAplha);
            }
        }
    }

    public void UpdateHealthUI()
    {
        if (newHealth != currentHealth)
        {
            if (newHealth < currentHealth)
                TakeDamage(currentHealth - newHealth);

            if (newHealth > currentHealth)
                RestoreHealth(newHealth - currentHealth);

            currentHealth = newHealth;
        }

        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float hFraction = (float)newHealth / (float)maxHealth;

        if (fillBack > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, hFraction, percentComplete);
        }

        if (fillFront < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, percentComplete);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        lerpTimer = 0f;
        durationTimer = 0f;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }

    public void RestoreHealth(int healAmount)
    {
        currentHealth += healAmount;
        lerpTimer = 0f;
    }
}
