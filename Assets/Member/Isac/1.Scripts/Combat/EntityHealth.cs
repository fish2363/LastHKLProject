using Member.Isac._1.Scripts.Entities;
using UnityEngine;

namespace Member.Isac._1.Scripts.Combat
{
    public class EntityHealth : MonoBehaviour, IEntityComponent, IDamageable, IAfterInit
    {
        private Entity _entity;
        private ActionData _actionData;

        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
        
        public void Initialize(Entity entity)
        {
        }

        public void AfterInit()
        {
            
        }

        public void ApplyDamage(DamageData damageData, Vector3 hitPoint, Vector3 hitNormal, AttackDataSO attackData, Entity dealer)
        {
        }
    }
}