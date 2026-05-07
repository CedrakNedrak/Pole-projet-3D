using UnityEngine;

public class Ressources : MonoBehaviour
{
    [Header("Ressources du joueur")]
    [SerializeField] private int gold = 0;

    public int GetGold()
    {
        return gold;
    }

    public void AddGold(int quantité)
    {
        if (quantité <= 0) return;

        gold += quantité;
    }

    public bool SpendGold(int quantité)
    {
        if (quantité <= 0) return false;

        if (gold >= quantité)
        {
            gold -= quantité;
            return true;
        }

        return false;
    }

    public void RemoveGold(int quantité)
    {
        if (quantité <= 0) return;

        gold -= quantité;

        if (gold < 0)
            gold = 0;
    }

    public void SetGold(int quantité)
    {
        gold = Mathf.Max(0, quantité);
    }
}