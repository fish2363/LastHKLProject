using Blade.Entities;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private EntityVFX _vfxCompo;
    private readonly string _footStepEffectName = "FootStep";
    public PlayerMoveState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        _vfxCompo = entity.GetCompo<EntityVFX>();
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputReader.OnCrouchPressed += CrouchHandler;
    }

    public override void Exit()
    {
        _player.InputReader.OnCrouchPressed -= CrouchHandler;
        base.Exit();
    }

    public void CrouchHandler()
    {
        _player.ChangeState("CROUCH");
    }

    public override void Update()
    {
        base.Update();
        Vector2 movementKey = _player.InputReader.MovementKey;
        _mover.SetMovementDirection(movementKey);
        if (movementKey.magnitude < _inputThreshold)
            _player.ChangeState("IDLE");
    }
}
