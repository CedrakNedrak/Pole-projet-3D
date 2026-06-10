using System.Collections.Generic;
using UnityEngine;

public class EntretienManager : MonoBehaviour
{
    [SerializeField] private Ressources ressources;

    private List<UnitEntretien> units = new List<UnitEntretien>();

    private float timer;

    private void Start()
    {
        if (ressources == null)
            ressources = FindObjectOfType<Ressources>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            RemoveGold();
            timer = 0f;
        }
    }

    public void RegisterUnit(UnitEntretien unit)
    {
        if (unit == null)
            return;

        if (!units.Contains(unit))
            units.Add(unit);
    }

    public void UnregisterUnit(UnitEntretien unit)
    {
        if (unit == null)
            return;

        if (units.Contains(unit))
            units.Remove(unit);
    }

    private void RemoveGold()
    {
        if (ressources == null)
            return;

        int totalCost = 0;

        for (int i = units.Count - 1; i >= 0; i--)
        {
            if (units[i] == null)
            {
                units.RemoveAt(i);
                continue;
            }

            totalCost += units[i].GoldCostPerSecond;
        }

        if (totalCost > 0)
            ressources.RemoveGold(totalCost);
    }
}