using UnityEngine;

public class PlayerCrouchState : PlayerState
{
    private CameraSettingComponent cameraSetting;
    private const float crouchHeight = 1f;
    private const float defalutHeight = 2f;

    public PlayerCrouchState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        cameraSetting = entity.GetCompo<CameraSettingComponent>();
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputReader.OnCrouchPressed += CancelCrouchHandler;
        _mover.StopImmediately();

        _mover.CanSprint = false;
        _mover.SetMoveSpeed(_mover.MoveSpeed /= 2);
        _mover.SetColliderHeight(crouchHeight);
        cameraSetting.ChangeCamera(true);
    }

    public override void Update()
    {
        base.Update();
        Vector2 movementKey = _player.InputReader.MovementKey;
        _mover.SetMovementDirection(movementKey);
    }

    public override void Exit()
    {
        cameraSetting.ChangeCamera(false);
        _player.InputReader.OnCrouchPressed -= CancelCrouchHandler;
        _mover.MoveSpeed *= 2;
        _mover.CanSprint = true;
        _mover.SetColliderHeight(defalutHeight);
        base.Exit();
    }


    public void CancelCrouchHandler()
    {
        _player.ChangeState("IDLE");
    }
}
