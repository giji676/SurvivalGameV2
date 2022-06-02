using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        Debug.Log(damage + " " + armor.GetValue());

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void RestoreHealth(int health)
    {
        //currentHealth = Mathf.Clamp(currentHealth, 0, int.MaxValue);
        currentHealth += health;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
    }
}
