using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StopToNav", story: "[Movement] Stop to [newValue]", category: "Action", id: "c57a7212025700821a87440ad91960e2")]
public partial class StopToNavAction : Action
{
    [SerializeReference] public BlackboardVariable<NavMovement> Movement;
    [SerializeReference] public BlackboardVariable<bool> NewValue;

    protected override Status OnStart()
    {
        Movement.Value.SetDestination(Movement.Value.transform.position);
        Movement.Value.SetStop(NewValue.Value);
        return Status.Success;
    }
}

