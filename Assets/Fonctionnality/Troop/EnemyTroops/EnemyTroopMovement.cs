using UnityEngine;

public class EnemyTroopMovement : TroopMovement
{
    public EnemyTroopMovement(TroopType troopType) : base(troopType) { }

    protected override void Start()
    {
        base.Start();
        EntityManager.Instance.RegisterEnemyTroop(this);
        StartMoving();
    }

    public void StartMoving()
    {
        int x = Random.Range(-2, 3);
        int y = Random.Range(-2, 3);
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + new Vector3(x, y, 0);
        Debug.Log($" is moving from {startPos} to {endPos}");
        base.StartMoving(startPos, endPos);
    }

    void Update()
    {
        if (HasFinishedMovement())
        {
            StartNextMovement();
        }
    }

    private bool HasFinishedMovement()
    {
        return (path == null || path.Count <= tweenEnCours);
    }

    public void StartNextMovement()
    {
        StopTween();
        tweenEnCours = 0;
        StartMoving();
    }

    public void ContinueMovement()
    {
        Rotate();
        StartMoving();
    }

    private void OnDestroy()
    {
        if (EntityManager.Instance != null)
        {
            EntityManager.Instance.UnregisterEnemyTroop(this);
        }
    }

    public override void ChangeTween()
    {
        if (tweenEnCours < path.Count - 1)
        {
            tweenEnCours += 1;
            StartTween(path[tweenEnCours]);
            Rotate();
        }
        else {
            StartMoving();
            tweenEnCours = 0; }
    }
}
