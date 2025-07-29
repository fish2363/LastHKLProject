using UnityEngine;

public class PlayerIdleState : PlayerState
{
    private CharacterMovement _movement;
    public PlayerIdleState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        _movement = entity.GetCompo<CharacterMovement>();
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
        _movement.SetMovementDirection(movementKey);
        if (movementKey.magnitude > _inputThreshold)
        {
            _player.ChangeState("MOVE");
        }
    }
}
