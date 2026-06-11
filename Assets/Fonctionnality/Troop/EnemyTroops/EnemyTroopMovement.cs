using UnityEngine;

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
        // While battling, the battle system fully owns movement (via
        // EndTweensThenStartMovingTowards), so the wandering loop must stay out
        // of the way to avoid stomping the pursuit tweens.
        if (isBattling)
            return;

        // Resume wandering whenever the troop is idle and not in combat.
        if (!isMoving)
        {
            StartNextMovement();
        }
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
        // Battle system requested a redirect: drop the current path and head to
        // the latest target. This is what makes pursuit follow a moving enemy.
        if (endTweens)
        {
            endTweens = false;
            StopTween();
            StartMoving(transform.position, nextEndPos);
            return;
        }

        if (tweenEnCours < path.Count - 1)
        {
            tweenEnCours += 1;
            StartTween(path[tweenEnCours]);
            Rotate();
        }
        else
        {
            // Path finished: mark idle so EndTweensThenStartMovingTowards (and the
            // wandering Update) can re-path, then wander again if not battling.
            tweenEnCours = 0;
            isMoving = false;
            ReStartMoving();
        }
    }
}
