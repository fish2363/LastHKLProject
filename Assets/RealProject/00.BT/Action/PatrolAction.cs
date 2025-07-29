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
        Initialize(); //nav 받아오기
        _navMovement.SetDestination(WayPoints.Value[_currentPointIdx].position); //현재 인덱스의 patrol 지점을 이동경로로 설정하기
        return Status.Running;
    }

    private void Initialize()
    {
        if (_navMovement == null)
            _navMovement = Self.Value.GetCompo<NavMovement>();

    }

    protected override Status OnUpdate()
    {
        if (_navMovement.IsArrived) //도착 시 석세스
            return Status.Success;
        return Status.Running;
    }

    protected override void OnEnd()
    {
        _currentPointIdx = (_currentPointIdx + 1) % WayPoints.Value.Length; // 현재 값에다가 최대값을 넘지 않는 선에서 1 더하기
    }
}

