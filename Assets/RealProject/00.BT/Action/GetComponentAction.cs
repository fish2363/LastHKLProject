using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetComponent", story: "Get Component from [Self]", category: "Action", id: "eea5c018b034a70ba7ec42f1323406b0")]
public partial class GetComponentAction : Action
{
    [SerializeReference] public BlackboardVariable<Monster> Self;

    protected override Status OnStart()
    {
        Monster monster = Self.Value;
        List<BlackboardVariable> varList = monster.BtAgent.BlackboardReference.Blackboard.Variables;

        foreach (var variable in varList)
        {
            if (typeof(IEntityComponent).IsAssignableFrom(variable.Type) == false) continue;

            SetVariable(monster, variable.Name, monster.GetCompo(variable.Type));
        }


        return Status.Success;
    }

    private void SetVariable(Monster enemy, string variableName, IEntityComponent component)
    {
        Debug.Assert(component != null, $"Check {variableName} component not exist on {enemy.gameObject.name}");
        if (enemy.BtAgent.GetVariable(variableName, out BlackboardVariable variable))
        {
            variable.ObjectValue = component;
        }
    }
}

