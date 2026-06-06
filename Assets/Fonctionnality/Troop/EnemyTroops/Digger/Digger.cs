using UnityEngine;

public class Digger : EnemyTroopMovement
{
    public Digger() : base(TroopType.MiningTroop) { }
    
    
    public override void ReStartMoving()
    {
        Vector3 mainPos = TileGenerator.tileGenerator.MainTownPosition;
        Debug.Log("Digger ReStartMoving to "  + mainPos);
        base.StartMoving(transform.position, mainPos);
    }
}

 