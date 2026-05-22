using UnityEngine;

public class EntityFaction : MonoBehaviour
{
    [SerializeField] private Faction faction;

    public Faction Faction => faction;

    public bool IsAlly()
    {
        return faction == Faction.Ally;
    }

    public bool IsEnemy()
    {
        return faction == Faction.Enemy;
    }

    public bool IsSameFaction(EntityFaction other)
    {
        if (other == null)
            return false;

        return faction == other.Faction;
    }

    public bool IsOppositeFaction(EntityFaction other)
    {
        if (other == null)
            return false;

        return faction != other.Faction;
    }
}