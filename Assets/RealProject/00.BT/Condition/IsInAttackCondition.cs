using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsInAttack", story: "[Self] check [Target] in attack range", category: "Conditions", id: "f9c29d971b4558ce15132f72b44e5f85")]
public partial class IsInAttackCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Monster> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    public override bool IsTrue()
    {
        if (Target.Value == null) return false;

        float distance = Vector3.Distance(Self.Value.transform.position, Target.Value.position);
        return distance < Self.Value.attackRange;
    }
}
