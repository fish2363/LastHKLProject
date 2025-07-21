using Blade.FSM;
using System;
using UnityEngine;

public class PlayerState : EntityState
{
    protected Player _player;
    protected CharacterMovement _mover;
    protected readonly float _inputThreshold = 0.1f;

    public PlayerState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        _player = entity as Player;
        _mover = entity.GetCompo<CharacterMovement>();
        Debug.Assert(_player != null, $"Player state using only in player");
    }
    public override void Enter()
    {
        base.Enter();
        
    }

}
