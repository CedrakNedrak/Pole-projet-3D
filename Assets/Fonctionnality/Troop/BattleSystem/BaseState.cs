using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : IState
{
    public virtual BattleStateMachine BattleStateMachine { get; set; }

    private float enemyDetectionRadius;
    private float pathRecalculationInterval = 0.5f;
    private float timeSinceLastPathRecalculation = 0f;

    public BaseState(BattleStateMachine battleStateMachine, float enemyDetectionRadius)
    {
        BattleStateMachine = battleStateMachine;
        this.enemyDetectionRadius = enemyDetectionRadius;
        timeSinceLastPathRecalculation = 0f;
    }
    public virtual MonoBehaviour Mono() => BattleStateMachine.troopContext.Mono();
    public virtual TroopMovement TroopMovement() => BattleStateMachine.troopContext.TroopMovement();
    public virtual Vector3 Position() => BattleStateMachine.troopContext.Position();
    public abstract Dictionary<Func<float, bool>, BaseState> TransitionConditions { get; }
    public virtual float EnemyDetectionRadius() => enemyDetectionRadius;
    public abstract void Enter();
    public float r { get; set; }
    public Collider ClosestEnemy { get; set; }

    public virtual void Update()
    {
        if (ClosestEnemy)
        {
            timeSinceLastPathRecalculation += Time.deltaTime;

            // Only recalculate path at regular intervals
            if (timeSinceLastPathRecalculation >= pathRecalculationInterval)
            {
                timeSinceLastPathRecalculation = 0f;
                TroopMovement().EndTweensThenStartMovingTowards(ClosestEnemy.transform.position);
            }
        }
    }
    public abstract void Exit();
}
