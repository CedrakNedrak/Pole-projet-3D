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
    public virtual TroopMovement TroopMovement() => BattleStateMachine.troopContext.TroopMovement();
    public virtual Vector3 Position() => BattleStateMachine.troopContext.Position();
    public abstract Dictionary<Func<float, bool>, BaseState> TransitionConditions { get; }
    public virtual float EnemyDetectionRadius() => enemyDetectionRadius;
    public abstract void Enter();
    public float r { get; set; }
    public Collider closestEnemy { get; set; }
    public virtual void Update()
    {
        TroopMovement().StartMoving(Position(), closestEnemy.transform.position);
    }
    public abstract void Exit();
}
