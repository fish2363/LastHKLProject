using System;
using Member.Isac._1.Scripts.Entities;
using Member.LCM._01.Script.Unit;
using UnityEngine;

namespace Member.Isac._1.Scripts.Combat
{
    public class EntityHealth : MonoBehaviour, IEntityComponent, IDamageable, IAfterInit
    {
        private Entity _entity;
        private ActionData _actionData;
        private EntityStatCompo _statCompo;


        [SerializeField] private StatSO hpStat;
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
        [SerializeField] private float defaultHealth;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _actionData = entity.GetCompo<ActionData>();
            _statCompo = entity.GetCompo<EntityStatCompo>();
        }

        public void AfterInit()
        {
            maxHealth = currentHealth = _statCompo.SubscribeStat(hpStat, HandleMaxHPChanged, defaultHealth);
        }

        private void OnDestroy()
        {
            _statCompo.UnSubscribeStat(hpStat, HandleMaxHPChanged);
        }

        private void HandleMaxHPChanged(StatSO stat, float currentValue, float previouseValue)
        {
            float changed = currentValue - previouseValue;
            maxHealth = currentValue;
            if (changed > 0)
                currentHealth = Mathf.Clamp(currentHealth + changed, 0 , maxHealth);
            else
                currentHealth = Mathf.Clamp(currentHealth, 0 , maxHealth);
        }

        public void ApplyDamage(DamageData damageData, Vector3 hitPoint, Vector3 hitNormal, AttackDataSO attackData, Entity dealer)
        {
            _actionData.HitNormal = hitNormal;
            _actionData.HitPoint = hitPoint;
            _actionData.HitByPowerAttack = attackData.isPowerAttack;
         
            currentHealth = Mathf.Clamp(currentHealth - damageData.damage, 0 , maxHealth);
            if (currentHealth <= 0)
            {
                _entity.OnDeathEvent?.Invoke();
            }

            _entity.OnHitEvent?.Invoke();
        }
    }
}