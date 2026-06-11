using System;
using System.Collections.Generic;

public class PursueState : BaseState
{
    public PursueState(BattleStateMachine battleStateMachine, float enemyDetectionRadius) : base(battleStateMachine, enemyDetectionRadius) { }
    public override Dictionary<Func<float, bool>, BaseState> TransitionConditions => new Dictionary<Func<float, bool>, BaseState>
    {
        {(r) => (r < BattleStateMachine.AttackState.AttackRange), BattleStateMachine.AttackState },
        {(r) => (r > EnemyDetectionRadius()), BattleStateMachine.AlertState },
    };
    public override float EnemyDetectionRadius() => 10f;
    public override void Enter()
    {
        TroopMovement().isBattling = true;
    }
    public override void Update()
    {
        base.Update();
    }
    public override void Exit() { }
}
