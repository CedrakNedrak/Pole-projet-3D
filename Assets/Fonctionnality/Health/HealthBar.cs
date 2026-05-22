using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float health;
    private float maxHealth;
    [SerializeField] private float barWidth;
    [SerializeField] private float barHeight;

    [SerializeField] private RectTransform healthBar;

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public void SetHealth(float health)
    {
        this.health = Mathf.Clamp(health, 0, maxHealth);
        float fill = (this.health / maxHealth) * barWidth;
        healthBar.sizeDelta = new Vector2(fill, barHeight);
    }

    public void ChangeHealth(float deltaHealth)
    {
        health = Mathf.Clamp(health + deltaHealth, 0, maxHealth);
        float fill = (health / maxHealth) * barWidth;
        healthBar.sizeDelta = new Vector2(fill, barHeight);
    }
}
