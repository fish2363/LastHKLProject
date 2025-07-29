using Blade.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChangeAnimation", story: "[Animator] change [oldValue] to [newValue]", category: "Action", id: "8d905100cf449265d4e823eccc841755")]
public partial class ChangeAnimationAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityAnimator> EntityAnimator;
    [SerializeReference] public BlackboardVariable<string> OldValue;
    [SerializeReference] public BlackboardVariable<string> NewValue;

    protected override Status OnStart()
    {
        EntityAnimator.Value.SetParam(Animator.StringToHash(OldValue.Value), false);
        EntityAnimator.Value.SetParam(Animator.StringToHash(NewValue.Value), true);

        OldValue.Value = NewValue.Value; //이전값을 새로운 값으로 갱신해야 된다.
        return Status.Success; //성공을 리턴하면 이걸로 끝이다.
    }
}

