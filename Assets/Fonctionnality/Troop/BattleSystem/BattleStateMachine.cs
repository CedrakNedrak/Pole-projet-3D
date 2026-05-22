using UnityEngine;

public class BattleStateMachine
{
    private BaseState currentState;
    public readonly ITroopContext troopContext;

    public readonly DefendState DefendState;
    public readonly AttackState AttackState;
    public readonly PursueState PursueState;
    public readonly AlertState AlertState;

    public BattleStateMachine(ITroopContext troopContext)
    {
        DefendState = new DefendState(this, 5f);
        AttackState = new AttackState(this, 5f, 3f);
        PursueState = new PursueState(this, 5f);
        AlertState = new AlertState(this, 5f, 2f);
        currentState = DefendState;
        this.troopContext = troopContext;
    }
    public void Update()
    {
        currentState.Update();
    }
    public void ChangeState(BaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public (Collider, float) IdentifyClosestEnemy()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(troopContext.Position(), currentState.EnemyDetectionRadius(), LayerMask.GetMask("Enemy"));
        float minDistance = float.MaxValue;
        Collider closestEnemy = null;
        foreach (Collider collider in enemiesInRange)
        {
            float distanceToPlayer = Vector3.Distance(collider.transform.position, troopContext.Position());
            if (distanceToPlayer < minDistance)
            {
                minDistance = distanceToPlayer;
                closestEnemy = collider;
            }
        }
        return (closestEnemy, minDistance);
    }
}
