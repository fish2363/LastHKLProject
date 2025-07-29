using UnityEngine;
using Unity.Behavior;
using Blade.Core;
using System;

public class Monster : Entity
{
    [field : SerializeField] public GameEventChannelSO SoundDetectSO { get; private set; }

    [HideInInspector] public Transform soundPoint;
    [HideInInspector] public bool isDetectSound;
    
    public BehaviorGraphAgent BtAgent => _btAgent;
    private BehaviorGraphAgent _btAgent;

    public float detectRange;
    public float attackRange;

    private IJumpscare jumpScareComponent;

    private void Start()
    {
        SoundDetectSO.AddListener<BigSoundDetection>(SoundCheck);
        
        BlackboardVariable<Transform> target = GetBlackboardVariable<Transform>("Target");
        target.Value = FindAnyObjectByType<Player>().transform;
        jumpScareComponent = GetCompo<JumpScareComponent>();
    } //임시 코드 : 나중에 Inject 시스템 공부 후 활용해서 PlayerFinder로 바꿀 것

    public void PlayerDead()
    {
        jumpScareComponent?.Excute();
    }

    private void SoundCheck(BigSoundDetection obj)
    {
        soundPoint = obj.soundPoint;
    }

    private void OnDisable()
    {
        SoundDetectSO.RemoveListener<BigSoundDetection>(SoundCheck);
    }

    protected override void AddComponents()
    {
        base.AddComponents();
        _btAgent = GetComponent<BehaviorGraphAgent>();
        Debug.Assert(_btAgent != null, $"{gameObject.name} does not have an BehaviorGraphAgent");
    }

    public BlackboardVariable<T> GetBlackboardVariable<T>(string key)
    {
        if (_btAgent.GetVariable(key, out BlackboardVariable<T> result))
            return result;
        return default;
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}