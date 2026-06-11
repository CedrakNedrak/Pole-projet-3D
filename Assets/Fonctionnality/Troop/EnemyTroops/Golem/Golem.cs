using UnityEngine;

public class Golem : EnemyTroopMovement
{
    public Golem() : base(TroopType.NormalTroop, Quaternion.Euler(0f, -90f, 0f) * Quaternion.Euler(0f, 0f, 90f)) { }

    public override void ReStartMoving()
    {
        //if (!isBattling)
        //{
        //    int x = UnityEngine.Random.Range(-5, 6);
        //    int y = UnityEngine.Random.Range(-5, 6);
        //    Vector3 startPos = transform.position;
        //    Vector3 endPos = startPos + new Vector3(x, y, 0);
        //    base.StartMoving(startPos, endPos);
        //}
    }
}
