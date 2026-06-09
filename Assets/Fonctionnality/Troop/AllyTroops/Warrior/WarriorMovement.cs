using UnityEngine;

public class WarriorMovement : AllyTroopMovement
{
    public WarriorMovement() : base(TroopType.NormalTroop, (Quaternion.Euler(-90, 0, 0) * Quaternion.Euler(0, 90, 0))) { }
}
