using UnityEngine;

public class TroopStateMachine : MonoBehaviour, ITroopContext
{
    private BattleStateMachine battleStateMachine;
    [SerializeField] private TroopMovement troopMovement;

    [SerializeField] float defendStateRadius = 2f;
    [SerializeField] float attackStateRadius = 5f;
    [SerializeField] float attackRange = 3f;
    [SerializeField] float attackPower = 10f;
    [SerializeField] float attackDelay = 2f;
    [SerializeField] float pursueStateRadius = 10f;
    [SerializeField] float alertStateRadius = 8f;
    [SerializeField] float alertStateDuration = 2f;
    [SerializeField] string enemyMask = string.Empty;

    private void Start()
    {
        battleStateMachine = new BattleStateMachine(this, enemyMask, defendStateRadius, attackStateRadius, attackRange, attackPower, attackDelay, pursueStateRadius, alertStateRadius, alertStateDuration);
    }

    private void Update()
    {
        battleStateMachine.Update();
    }

    public Vector3 Position() => transform.position;
    public MonoBehaviour Mono() => this;
    public TroopMovement TroopMovement() => troopMovement;
}
