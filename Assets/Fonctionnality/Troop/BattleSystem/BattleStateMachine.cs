using System;
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
        DefendState = new DefendState(this, 2f);
        AttackState = new AttackState(this, 5f, 3f, 10f, 2f);
        PursueState = new PursueState(this, 10f);
        AlertState = new AlertState(this, 8f, 2f);
        currentState = DefendState;
        this.troopContext = troopContext;
    }
    public void Update()
    {
        ChangeStateBasedOnConditions();
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

    public void ChangeStateBasedOnConditions()
    {
        var (closestEnemy, r) = IdentifyClosestEnemy();
        currentState.r = r;
        currentState.ClosestEnemy = closestEnemy;
        foreach (Func<float, bool> condition in currentState.TransitionConditions.Keys)
        {
            if (condition(r))
            {
                BaseState nextState = currentState.TransitionConditions[condition];
                ChangeState(nextState);
                break;
            }
        }
    }
}
