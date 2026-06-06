using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public float AttackRange;
    public float AttackPower;
    public AttackState(BattleStateMachine battleStateMachine, float enemyDetectionRadius, float attackRange, float attackPower) : base(battleStateMachine, enemyDetectionRadius)
    {
        this.AttackRange = attackRange;
        this.AttackPower = attackPower;
    }
    public override Dictionary<Func<float, bool>, BaseState> TransitionConditions => new Dictionary<Func<float, bool>, BaseState>
    {
        {(r) => (r> EnemyDetectionRadius()), BattleStateMachine.PursueState}
    };
    public override float EnemyDetectionRadius() => 10f;
    public override void Enter() { }
    public override void Update()
    {
        if (r < AttackRange)
        {
            Attack(closestEnemy);
        }
        else
        {
            base.Update();
        }
    }
    public override void Exit() { }

    private void Attack(Collider enemyCollider)
    {
        TroopHealth enemyHealth = enemyCollider.GetComponent<TroopHealth>();
        enemyHealth.TakeDamage(AttackPower);
    }
}
