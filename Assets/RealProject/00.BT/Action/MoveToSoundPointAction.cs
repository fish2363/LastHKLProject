using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToSoundPoint", story: "[Self] move to soundPoint", category: "Action", id: "14efdd7558772c83e315e3f764b3cd22")]
public partial class MoveToSoundPointAction : Action
{
    [SerializeReference] public BlackboardVariable<Monster> Self;
    private NavMovement _navMovement;

    protected override Status OnStart()
    {
        if (Self.Value.soundPoint == null) return Status.Failure;
        Initialize(); //nav �޾ƿ���
        _navMovement.SetDestination(Self.Value.soundPoint.position); //���� �ε����� patrol ������ �̵���η� �����ϱ�
        return Status.Running;
    }

    private void Initialize()
    {
        if (_navMovement == null)
            _navMovement = Self.Value.GetCompo<NavMovement>();
    }

    protected override Status OnUpdate()
    {
        if (_navMovement.IsArrived) //���� �� ������
            return Status.Success;
        return Status.Running;
    }
}

