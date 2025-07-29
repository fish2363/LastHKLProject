using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsInSight", story: "[Self] check [Target] in detect range", category: "Conditions", id: "020d92ccc3cdf10ce6c5388521df9740")]
public partial class IsInSightCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Monster> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    public override bool IsTrue()
    {
        if (Target.Value == null) return false;

        float distance = Vector3.Distance(Self.Value.transform.position, Target.Value.position);
        return distance < Self.Value.detectRange;
    }
}
