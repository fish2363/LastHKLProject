using UnityEngine;
using UnityEngine.AI;

public class NavMovement : MonoBehaviour, IEntityComponent,IAfterInit
{
    [field: SerializeField] public float MoveSpeed { get; set; }

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float stopOffset = 0.05f; //거리에 대한 오프셋
    [SerializeField] private float rotateSpeed = 10f;

    private Entity _entity;
    private Transform _lookAtTrm;

    public bool IsArrived => !agent.pathPending && agent.remainingDistance < agent.stoppingDistance + stopOffset;
    public float RemainDistance => agent.pathPending ? -1 : agent.remainingDistance; //경로 계산이 진행중이면 -1, 완료되면 남은 거리를 반환
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
        if (_lookAtTrm != null) LookAtTarget(_lookAtTrm.position, true);// 바라봐야 하는 타깃이 있으면 바라본다.
        else if (agent.hasPath && agent.isStopped == false) LookAtTarget(agent.steeringTarget); //경로를 향해 움직이는 상태라면 내가 가는 곳을 바라본다
    }

    /// <summary>
    /// 바라봐야할 최종 로테이션을 반환합니다.
    /// </summary>
    /// <param name="target">바라볼 목표지점을 넣습니다. y축은 무시</param>
    /// <param name="isSmooth">부드럽게 돌아갈 것인지 결정합니다.</param>
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
