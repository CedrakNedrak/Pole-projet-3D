using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Instance;

    public List<TroopMovement> allyTroops = new List<TroopMovement>();
    public List<TroopMovement> enemyTroops = new List<TroopMovement>();


    private void Awake()
    {
        Instance = this;
    }

    public void RegisterAllyTroop(TroopMovement allyTroop)
    {
        allyTroops.Add(allyTroop);
    }

    public void UnregisterAllyTroop(TroopMovement allyTroop)
    {
        allyTroops.Remove(allyTroop);
    }

    public void RegisterEnemyTroop(TroopMovement enemyTroop)
    {
        enemyTroops.Add(enemyTroop);
    }

    public void UnregisterEnemyTroop(TroopMovement enemyTroop)
    {
        enemyTroops.Remove(enemyTroop);
    }

}
