using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : BaseState
{
    private float stayInAlertDuration; // Duration to stay in alert state
    public AlertState(BattleStateMachine battleStateMachine, float enemyDetectionRadius, float stayInAlertDuration) : base(battleStateMachine, enemyDetectionRadius)
    {
        this.stayInAlertDuration = stayInAlertDuration;
    }
    public override Dictionary<Func<float, bool>, BaseState> TransitionConditions => new Dictionary<Func<float, bool>, BaseState>
    {
        {(r) => (r < EnemyDetectionRadius()), BattleStateMachine.PursueState }
    };

    public override float EnemyDetectionRadius() => 10f;
    public override void Enter()
    {
        TroopMovement().isBattling = false;
        Mono().StartCoroutine(StayInAlert());
    }
    public override void Update()
    {
        base.Update();
    }
    public override void Exit() { }

    private IEnumerator StayInAlert()
    {
        yield return new WaitForSeconds(stayInAlertDuration);
        if (BattleStateMachine.currentState == BattleStateMachine.AlertState)//change, it has maybe changed to another state then back to alertState
        {
            BattleStateMachine.ChangeState(BattleStateMachine.DefendState);
        }
    }
}
