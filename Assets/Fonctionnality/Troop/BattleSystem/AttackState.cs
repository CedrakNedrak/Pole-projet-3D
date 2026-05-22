using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public float AttackRange;
    public AttackState(BattleStateMachine battleStateMachine, float enemyDetectionRadius, float attackRange) : base(battleStateMachine, enemyDetectionRadius)
    {
        this.AttackRange = attackRange;
    }
    public override Dictionary<Func<float, bool>, BaseState> TransitionConditions => new Dictionary<Func<float, bool>, BaseState>
    {
        {(r) => (r> EnemyDetectionRadius()), BattleStateMachine.PursueState}
    };
    public override float EnemyDetectionRadius() => 10f;
    public override void Enter() { }
    public override void Update()
    {
        base.Update();
        if (r < AttackRange)
        {
            Attack(closestEnemy);
        }
    }
    public override void Exit() { }

    private void Attack(Collider enemyCollider)
    {
        // Implement attack logic here
    }
}
