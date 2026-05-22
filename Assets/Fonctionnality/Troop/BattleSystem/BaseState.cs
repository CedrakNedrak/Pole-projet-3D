using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : IState
{
    public virtual BattleStateMachine BattleStateMachine { get; set; }

    private float enemyDetectionRadius;
    public BaseState(BattleStateMachine battleStateMachine, float enemyDetectionRadius)
    {
        BattleStateMachine = battleStateMachine;
        this.enemyDetectionRadius = enemyDetectionRadius;
    }
    public virtual MonoBehaviour Mono() => BattleStateMachine.troopContext.Mono();
    public abstract Dictionary<Func<float, bool>, BaseState> TransitionConditions { get; }
    public virtual float EnemyDetectionRadius() => enemyDetectionRadius;
    public abstract void Enter();
    public float r { get; set; }
    public Collider closestEnemy { get; set; }
    public virtual void Update()
    {
        var (closestEnemy, r) = BattleStateMachine.IdentifyClosestEnemy();
        this.r = r;
        this.closestEnemy = closestEnemy;
        foreach (Func<float, bool> condition in TransitionConditions.Keys)
        {
            if (condition(r))
            {
                BaseState nextState = TransitionConditions[condition];
                BattleStateMachine.ChangeState(nextState);
                break;
            }
        }
    }
    public abstract void Exit();
}
