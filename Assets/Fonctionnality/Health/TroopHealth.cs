using UnityEngine;

public class TroopHealth : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float maxHealth;
    private float health;

    private void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        healthBar.ChangeHealth(-damage);
        health = healthBar.health;
    }

    private void Update()
    {
        if (Input.GetKeyDown("d"))
        {
            TakeDamage(10);
        }
    }

}
