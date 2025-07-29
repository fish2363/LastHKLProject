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
    [CreateProperty] private float m_Timer = 0.0f; //������Ƽ�� �����

    protected override Status OnStart()
    {
        m_Timer = SecondToWait; //Ÿ�̸ӿ� �ð� ����ֱ�
        if (m_Timer <= 0.0f) //���� 0 ���ϸ� �ٷ� ����
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        m_Timer -= Time.deltaTime; // �ð� �� ���
        if (m_Timer <= 0) //0���ϰ� �Ǹ� ����
        {
            return Status.Success;
        }

        return Status.Running;
    }
}

