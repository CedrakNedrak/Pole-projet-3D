using UnityEngine;

public class TroopHealth : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float maxHealth;
    [SerializeField] private CharaMovement charaMovement;
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
        if (healthBar.health <= 0)
        {
            charaMovement.StopTween();
            Destroy(gameObject);
        }
    }
}
