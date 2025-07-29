using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Patrol", story: "[Self] patrol with [WayPoints]", category: "Action", id: "5d28166fe582c8860b15026ec4095002")]
public partial class PatrolAction : Action
{
    [SerializeReference] public BlackboardVariable<Monster> Self;
    [SerializeReference] public BlackboardVariable<WayPoints> WayPoints;

    private int _currentPointIdx;
    private NavMovement _navMovement;

    protected override Status OnStart()
    {
        Initialize(); //nav �޾ƿ���
        _navMovement.SetDestination(WayPoints.Value[_currentPointIdx].position); //���� �ε����� patrol ������ �̵���η� �����ϱ�
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

    protected override void OnEnd()
    {
        _currentPointIdx = (_currentPointIdx + 1) % WayPoints.Value.Length; // ���� �����ٰ� �ִ밪�� ���� �ʴ� ������ 1 ���ϱ�
    }
}

