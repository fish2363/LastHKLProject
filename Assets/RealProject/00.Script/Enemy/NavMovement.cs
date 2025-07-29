using UnityEngine;
using UnityEngine.AI;

public class NavMovement : MonoBehaviour, IEntityComponent,IAfterInit
{
    [field: SerializeField] public float MoveSpeed { get; set; }

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float stopOffset = 0.05f; //�Ÿ��� ���� ������
    [SerializeField] private float rotateSpeed = 10f;

    private Entity _entity;
    private Transform _lookAtTrm;

    public bool IsArrived => !agent.pathPending && agent.remainingDistance < agent.stoppingDistance + stopOffset;
    public float RemainDistance => agent.pathPending ? -1 : agent.remainingDistance; //��� ����� �������̸� -1, �Ϸ�Ǹ� ���� �Ÿ��� ��ȯ
    public Vector3 Velocity => agent.velocity;

    public bool UpdateRotation
    {
        get => agent.updateRotation;
        set => agent.updateRotation = value;
    }

    public void Initialize(Entity entity)
    {
        _entity = entity;
    }

    public void AfterInit()
    {
        agent.speed = MoveSpeed;
    }

    public void SetLookAtTarget(Transform target)
    {
        _lookAtTrm = target;
        UpdateRotation = target == null;
    }

    private void Update()
    {
        if (_lookAtTrm != null) LookAtTarget(_lookAtTrm.position, true);// �ٶ���� �ϴ� Ÿ���� ������ �ٶ󺻴�.
        else if (agent.hasPath && agent.isStopped == false) LookAtTarget(agent.steeringTarget); //��θ� ���� �����̴� ���¶�� ���� ���� ���� �ٶ󺻴�
    }

    /// <summary>
    /// �ٶ������ ���� �����̼��� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="target">�ٶ� ��ǥ������ �ֽ��ϴ�. y���� ����</param>
    /// <param name="isSmooth">�ε巴�� ���ư� ������ �����մϴ�.</param>
    /// <returns></returns>
    public Quaternion LookAtTarget(Vector3 target, bool isSmooth = true)
    {
        Vector3 direction = target - _entity.transform.position;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        if (isSmooth)
        {
            _entity.transform.rotation = Quaternion.Slerp(_entity.transform.rotation,
                                            lookRotation, Time.deltaTime * rotateSpeed);
        }
        else
        {
            _entity.transform.rotation = lookRotation;
        }

        return lookRotation;
    }

    public void SetStop(bool isStop) => agent.isStopped = isStop;
    public void SetVelocity(Vector3 velocity) => agent.velocity = velocity;
    public void SetSpeed(float speed) => agent.speed = speed;
    public void SetDestination(Vector3 destination) => agent.SetDestination(destination);
    public bool IsEntityOnNavmesh() => agent.isOnNavMesh;
    public void WarpToPosition(Vector3 position) => agent.Warp(position);
}
