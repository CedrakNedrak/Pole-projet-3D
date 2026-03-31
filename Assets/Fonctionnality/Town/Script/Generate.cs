using UnityEngine;

public class Generate : MonoBehaviour
{
    [Header("Production")]
    [SerializeField] private int goldPerSecond = 5;

    [Header("Références")]
    [SerializeField] private Ressources ressources;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            GenerateGold();
            timer = 0f;
        }
    }

    void GenerateGold()
    {
        if (ressources != null)
        {
            ressources.AddGold(goldPerSecond);
        }
    }
}