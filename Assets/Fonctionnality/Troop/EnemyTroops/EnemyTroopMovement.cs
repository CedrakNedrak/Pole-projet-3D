using UnityEngine;

public class EnemyTroopMovement : TroopMovement
{
    public EnemyTroopMovement(TroopType troopType) : base(troopType) { }

    protected override void Start()
    {
        base.Start();

        if (EntityManager.Instance != null)
        {
            EntityManager.Instance.RegisterEnemyTroop(this);
        }
        else
        {
            Debug.LogError("EntityManager.Instance is null! Make sure EntityManager is initialized before EnemyTroopMovement.");
        }

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
        else {
            ReStartMoving();
            tweenEnCours = 0; }
    }
}
