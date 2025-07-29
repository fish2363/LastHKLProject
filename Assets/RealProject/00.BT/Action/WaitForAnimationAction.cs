using Blade.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitForAnimation", story: "wait for end [Trigger]", category: "Action", id: "0985bb4a8bd1dcdf8b8f195e9a45633c")]
public partial class WaitForAnimationAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityAnimatorTrigger> Trigger;

    private bool _isTriggered; //트리거 신호 감지 불값

    protected override Status OnStart()
    {
        _isTriggered = false;
        Trigger.Value.OnAnimationEndTrigger += HandleAnimationEnd; //애니메이션 트리거를 구독
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (_isTriggered) // 트리거 콜이 발동될때까지 대기
            return Status.Success;
        return Status.Running;
    }

    protected override void OnEnd()
    {
        Trigger.Value.OnAnimationEndTrigger -= HandleAnimationEnd; // 구독 해제
    }

    private void HandleAnimationEnd() => _isTriggered = true; // 트리거 신호 옴
}

