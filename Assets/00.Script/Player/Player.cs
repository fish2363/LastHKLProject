using Blade.Core;
using Blade.FSM;
using Member.Isac._1.Scripts.Entities;
using UnityEngine;

public class Player : Entity
{
    [field: SerializeField] public GameEventChannelSO PlayerChannel { get; private set; }
    [field: SerializeField] public InputReader InputReader { get; private set; }

    [SerializeField] private StateDataSO[] stateDataList;

    private StateMachine _stateMachine;

    protected override void Awake()
    {
        base.Awake();
        _stateMachine = new StateMachine(this, stateDataList);

        OnDeathEvent.AddListener(HandleDeadEvent);
    }


    private void Start()
    {
        _stateMachine.ChangeState("IDLE");
    }

    private void Update()
    {
        _stateMachine.UpdateStateMachine();
    }

    public void ChangeState(string newStateName, bool force = false)
        => _stateMachine.ChangeState(newStateName, force);


    private void HandleDeadEvent()
    {
        if (IsDead) return;
        IsDead = true;
        PlayerChannel?.RaiseEvent(PlayerEvents.PlayerDead);
        ChangeState("DEAD", true);
    }
}