using UnityEngine;

public class EnemyTroopMovement : TroopMovement
{
    public EnemyTroopMovement(TroopType troopType) : base(troopType) { }

    protected override void Start()
    {
        base.Start();
        StartMoving();
        EntityManager.Instance.RegisterEnemyTroop(this);
    }

    public void StartMoving()
    {
        int x = Random.Range(0, 200);
        int y = Random.Range(0, 200);
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(x, y, 0);
        base.StartMoving(startPos, endPos);
    }

    void Update()
    {
        if (IsBumpingAgainstWall() || HasFinishedMovement())
        {
            StartNextMovement();
        }
        else
        {
            ContinueMovement();
        }
    }

    private bool HasFinishedMovement()
    {
        return (path != null && path.Count > tweenEnCours);
    }

    public bool IsBumpingAgainstWall()
    {
        Vector2Int targetNextPos = new Vector2Int(path[tweenEnCours].x, path[tweenEnCours].y);
        if (TileGenerator.tileGenerator.NormalWorldIntMatrice[targetNextPos.x, targetNextPos.y] == -1)
        {
            return true;
        }
        return false;
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
}
