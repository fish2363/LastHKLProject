using Member.Isac._1.Scripts.Entities;
using UnityEngine;

namespace Member.Isac._1.Scripts.Combat
{
    public interface IDamageable
    {
        public void ApplyDamage(DamageData damageData, Vector3 hitPoint, Vector3 hitNormal, AttackDataSO attackData, Entity dealer);
    }
}