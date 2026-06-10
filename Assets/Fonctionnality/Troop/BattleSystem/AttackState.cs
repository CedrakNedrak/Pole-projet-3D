using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public float AttackRange;
    public float AttackPower;
    public float AttackDelay;
    private float timeSinceLastAttack;
    public AttackState(BattleStateMachine battleStateMachine, float enemyDetectionRadius, float attackRange, float attackPower, float attackDelay) : base(battleStateMachine, enemyDetectionRadius)
    {
        this.AttackRange = attackRange;
        this.AttackPower = attackPower;
        this.AttackDelay = attackDelay;
    }
    public override Dictionary<Func<float, bool>, BaseState> TransitionConditions => new Dictionary<Func<float, bool>, BaseState>
    {
        {(r) => (r> EnemyDetectionRadius()), BattleStateMachine.PursueState}
    };
    public override float EnemyDetectionRadius() => 10f;
    public override void Enter()
    {
        timeSinceLastAttack = AttackDelay;//so that the troop can attack as soon as it enters Attack State
    }
    public override void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        if (ClosestEnemy != null && r < AttackRange && (timeSinceLastAttack >= AttackDelay))
        {
            timeSinceLastAttack = 0;
            Attack(ClosestEnemy);
        }
        else
        {
            base.Update();
        }
    }
    public override void Exit() { }

    private void Attack(Collider enemyCollider)
    {
        TroopHealth enemyHealth = enemyCollider.gameObject.GetComponent<TroopHealth>();

        if (enemyHealth == null)
        {
            Debug.LogError("AttackState.Attack: TroopHealth component not found on enemy: " + enemyCollider.gameObject.name);
            return;
        }

        enemyHealth.TakeDamage(AttackPower);
    }
}
