using System;
using UnityEngine;

public class BattleStateMachine
{
    public BaseState currentState;
    public readonly ITroopContext troopContext;

    public readonly DefendState DefendState;
    public readonly AttackState AttackState;
    public readonly PursueState PursueState;
    public readonly AlertState AlertState;

    private string enemyMask;

    public BattleStateMachine(ITroopContext troopContext, string enemyMask, float defendStateRadius, float attackStateRadius, float attackRange, float attackPower, float attackDelay, float pursueStateRadius, float alertStateRadius, float alertStateDuration)
    {
        this.enemyMask = enemyMask;
        DefendState = new DefendState(this, defendStateRadius);
        AttackState = new AttackState(this, attackStateRadius, attackRange, attackPower, attackDelay);
        PursueState = new PursueState(this, pursueStateRadius);
        AlertState = new AlertState(this, alertStateRadius, alertStateDuration);
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
        Collider[] enemiesInRange = Physics.OverlapSphere(troopContext.Position(), currentState.EnemyDetectionRadius(), LayerMask.GetMask(enemyMask));
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
