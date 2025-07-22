using UnityEngine;

public class PlayerCrouchState : PlayerState
{
    private CameraSettingComponent cameraSetting;

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
        _mover.MoveSpeed /= 2;
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
        _player.InputReader.OnCrouchPressed -= CancelCrouchHandler;
        _mover.MoveSpeed *= 2;
        _mover.CanSprint = true;
        cameraSetting.ChangeCamera(false);
        base.Exit();
    }


    public void CancelCrouchHandler()
    {
        _player.ChangeState("IDLE");
    }
}
