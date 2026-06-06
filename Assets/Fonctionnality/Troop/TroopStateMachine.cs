using UnityEngine;

public class TroopStateMachine : MonoBehaviour, ITroopContext
{
    private BattleStateMachine battleStateMachine;

    private void Start()
    {
        battleStateMachine = new BattleStateMachine(this);
    }

    private void Update()
    {
        battleStateMachine.Update();
    }

    public Vector3 Position() => transform.position;
    public MonoBehaviour Mono() => this;
    public TroopMovement TroopMovement() => GetComponent<TroopMovement>();
}
