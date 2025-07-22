using Blade.FSM;
using System;
using UnityEngine;

public class PlayerState : EntityState
{
    protected Player _player;
    protected CharacterMovement _mover;
    protected readonly float _inputThreshold = 0.1f;
    private HeadBobController headBob;

    private const float sprintFrequency = 16.3f;
    private const float walkFrequency = 10.5f;

    public PlayerState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        _player = entity as Player;
        _mover = entity.GetCompo<CharacterMovement>();
        headBob = entity.GetCompo<HeadBobController>();
        Debug.Assert(_player != null, $"Player state using only in player");
    }
    public override void Enter()
    {
        base.Enter();
        _player.InputReader.OnSprintPressed += SprintHandler;
    }

    private void SprintHandler(bool obj) {
        if (_mover.CanSprint)
        {
            _mover.MoveSpeed = obj ? 6f : 3f;
            headBob.SetFrequency(obj ? sprintFrequency : walkFrequency);
        }
    }

    public override void Exit()
    {
        _player.InputReader.OnSprintPressed -= SprintHandler;
        base.Exit();
    }
}
