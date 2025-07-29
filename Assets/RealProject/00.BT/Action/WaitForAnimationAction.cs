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

    private bool _isTriggered; //Ʈ���� ��ȣ ���� �Ұ�

    protected override Status OnStart()
    {
        _isTriggered = false;
        Trigger.Value.OnAnimationEndTrigger += HandleAnimationEnd; //�ִϸ��̼� Ʈ���Ÿ� ����
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (_isTriggered) // Ʈ���� ���� �ߵ��ɶ����� ���
            return Status.Success;
        return Status.Running;
    }

    protected override void OnEnd()
    {
        Trigger.Value.OnAnimationEndTrigger -= HandleAnimationEnd; // ���� ����
    }

    private void HandleAnimationEnd() => _isTriggered = true; // Ʈ���� ��ȣ ��
}

