using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToTarget", story: "[Movement] move to [Target]", category: "Action", id: "b9d4453627ddc13dbc170e5e82ad06ca")]
public partial class MoveToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<NavMovement> Movement;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    protected override Status OnStart()
    {
        Movement.Value.SetDestination(Target.Value.position); //�÷��̾� ������ �̵���� ����
        return Status.Success;
    }
}

