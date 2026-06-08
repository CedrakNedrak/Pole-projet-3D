using UnityEngine;

public class Golem : EnemyTroopMovement
{
    public Golem() : base(TroopType.NormalTroop) { }

    public override void ReStartMoving()
    {
        int x = UnityEngine.Random.Range(-5, 6);
        int y = UnityEngine.Random.Range(-5, 6);
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + new Vector3(x, y, 0);
        base.StartMoving(startPos, endPos);
    }
}
