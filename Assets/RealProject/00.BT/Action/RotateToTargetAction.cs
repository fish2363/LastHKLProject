using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RotateToTarget", story: "[Movement] rotate [Self] to [Target] immediately [IsRightNow]", category: "Action", id: "526b0c14b9ddf66657202ecdc6896139")]
public partial class RotateToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<NavMovement> Movement;
    [SerializeReference] public BlackboardVariable<Transform> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<bool> IsRightNow = new BlackboardVariable<bool>(false);

    protected override Status OnUpdate()
    {
        if (IsRightNow.Value)
        {
            Movement.Value.LookAtTarget(Target.Value.position, false);
            return Status.Success;
        }
        if (LookTargetSmoothly())
            return Status.Success;

        return Status.Running;
    }

    private bool LookTargetSmoothly()
    {
        Quaternion targetRot = Movement.Value.LookAtTarget(Target.Value.position); //회전을 하겠지.
        const float angleThreshold = 5f;
        return Quaternion.Angle(targetRot, Self.Value.rotation) < angleThreshold;
    }
}

