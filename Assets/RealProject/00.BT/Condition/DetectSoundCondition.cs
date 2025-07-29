using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "DetectSound", story: "[Self] heard a loud noise", category: "Conditions", id: "2ff3040f0088c4e17b1aaab682cec77c")]
public partial class DetectSoundCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Monster> Self;

    public override bool IsTrue()
    {
        if(Self.Value.isDetectSound && Self.Value.soundPoint != null)
        {
            Self.Value.isDetectSound = false;
            return true;
        }
        return false;
    }
}
