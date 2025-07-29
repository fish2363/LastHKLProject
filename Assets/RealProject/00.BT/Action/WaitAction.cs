using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Wait", story: "wait for [SecondToWait] second", category: "Action", id: "791e77a4b3843ceb3050b39d8b9b2929")]
public partial class WaitAction : Action
{
    [SerializeReference] public BlackboardVariable<float> SecondToWait;
    [CreateProperty] private float m_Timer = 0.0f; //프로퍼티로 만들기

    protected override Status OnStart()
    {
        m_Timer = SecondToWait; //타이머에 시간 집어넣기
        if (m_Timer <= 0.0f) //만약 0 이하면 바로 종료
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        m_Timer -= Time.deltaTime; // 시간 초 깎기
        if (m_Timer <= 0) //0이하가 되면 종료
        {
            return Status.Success;
        }

        return Status.Running;
    }
}

