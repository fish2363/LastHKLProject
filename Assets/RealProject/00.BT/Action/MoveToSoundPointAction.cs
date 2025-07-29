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
        Initialize(); //nav 받아오기
        _navMovement.SetDestination(Self.Value.soundPoint.position); //현재 인덱스의 patrol 지점을 이동경로로 설정하기
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
}

