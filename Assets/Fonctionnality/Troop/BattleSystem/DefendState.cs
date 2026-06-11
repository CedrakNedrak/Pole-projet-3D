using System;
using System.Collections.Generic;

public class DefendState : BaseState
{
    public DefendState(BattleStateMachine battleStateMachine, float enemyDetectionRadius) : base(battleStateMachine, enemyDetectionRadius) { }

    public override Dictionary<Func<float, bool>, BaseState> TransitionConditions => new Dictionary<Func<float, bool>, BaseState>
    {
        {(r) => (r < EnemyDetectionRadius()), BattleStateMachine.PursueState }
    };
    public override float EnemyDetectionRadius() => 10f;
    public override void Enter()
    {
        TroopMovement().isBattling = false;
    }
    public override void Update()
    {
        base.Update();
    }
    public override void Exit() { }
}
