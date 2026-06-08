using UnityEngine;
using UnityEngine.UI;

public class HealthTown : MonoBehaviour
{
    [SerializeField] float vieMax = 100f;
    private float vieActuelle;

    [SerializeField] private GameObject barreVie;
    private HealthBar healthBar;

    void Start()
    {
        healthBar = barreVie.GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(vieMax);
        vieActuelle = vieMax;
        MettreAJourBarreVie();
    }

    public void SubirDegats(float degats)
    {
        vieActuelle -= degats;
        vieActuelle = Mathf.Max(vieActuelle, 0);

        healthBar.ChangeHealth(-degats);

        if (vieActuelle <= 0)
        {
            DetruireTour();
        }
    }

    void MettreAJourBarreVie()
    {
        healthBar.ChangeHealth(vieMax);
    }

    void DetruireTour()
    {
        Destroy(gameObject);
    }
}

