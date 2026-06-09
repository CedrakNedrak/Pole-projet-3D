public class EnemyTroopMovement : TroopMovement
{
    public EnemyTroopMovement(TroopType troopType, Quaternion rotationToFaceRight) : base(troopType, rotationToFaceRight) { }

    protected override void Start()
    {
        base.Start();
        EntityManager.Instance.RegisterEnemyTroop(this);
        ReStartMoving();
    }

    public virtual void ReStartMoving()
    {
        base.StartMoving(transform.position, transform.position);
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
        ReStartMoving();
    }

    public void ContinueMovement()
    {
        Rotate();
        ReStartMoving();
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
        else
        {
            ReStartMoving();
            tweenEnCours = 0;
        }
    }
}
