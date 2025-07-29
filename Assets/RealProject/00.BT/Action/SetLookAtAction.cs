    using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetLookAt", story: "Set [Target] to [Navmovement]", category: "Action", id: "68e356b1c77f37176667c77ef187f966")]
public partial class SetLookAtAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<NavMovement> Navmovement;

    protected override Status OnStart()
    {
        Navmovement.Value.SetLookAtTarget(Target.Value);
        return Status.Success;
    }
}

