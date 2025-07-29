using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetVariableValue", story: "set [Variable] value to [Value]", category: "Action/Blackboard", id: "6574911a8616cccffc4256407aa79130")]
public partial class SetVariableValueAction : Action
{
    [SerializeReference] public BlackboardVariable Variable;
    [SerializeReference] public BlackboardVariable Value;
    protected override Status OnStart()
    {
        if (Variable == null || Value == null)
        {
            return Status.Failure;
        }
        Variable.ObjectValue = Value.ObjectValue;
        return Status.Success;
    }
}

