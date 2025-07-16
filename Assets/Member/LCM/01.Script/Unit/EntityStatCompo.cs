using System.Collections.Generic;
using System.Linq;
using Member.Isac._1.Scripts.Entities;
using UnityEngine;

namespace Member.LCM._01.Script.Unit
{
    public class EntityStatCompo : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private StatOverride[] statOverrides;
        //private StatSO[] _stats; //진짜 스탯들
        private Dictionary<string, StatSO> _stats;
        public Entity Owner { get; private set; } //밖에서 참조 가능하게
        public void Initialize(Entity entity)
        {
            Owner = entity;
            _stats = statOverrides.ToDictionary(s => s.Stat.statName, s=>s.CreateStat());
        }

        public StatSO GetStat(StatSO stat)
        {
            Debug.Assert(stat != null, $"Stat: GetStat - stat can not be null");
            return _stats.GetValueOrDefault(stat.statName);
        }

        public bool TryGetStat(StatSO stat, out StatSO outStat)
        {
            Debug.Assert(stat != null, $"Stats: TryGetStat - stat cannot be null");
            outStat = _stats.GetValueOrDefault(stat.statName);
            return outStat != null;
        }

        public void SetStatValue(StatSO stat, float value) => GetStat(stat).StatValue = value;
        public float GetStatValue(StatSO stat) => GetStat(stat).StatValue;
        public void IncreaseStatValue(StatSO stat, float value) => GetStat(stat).StatValue += value;

        public void AddModifier(StatSO stat, object key, float value)
            => GetStat(stat).AddModifier(key, value);

        public void RemoveModifier(StatSO stat, object key)
            => GetStat(stat).RemoveModifier(key);

        public void ClearAllStatModifier()
        {
            foreach (StatSO stat in _stats.Values)
            {
                stat.ClearModifier();
            }
        }

        public float SubscribeStat(StatSO stat, StatSO.ValueChangeHandler handler, float defaultValue)
        {
            StatSO target = GetStat(stat);
            if (target == null) return defaultValue;
            target.OnValueChanged += handler;
            return target.Value;
        }

        public void UnSubscribeStat(StatSO stat, StatSO.ValueChangeHandler handler)
        {
            StatSO target = GetStat(stat);
            if (target == null) return;
            target.OnValueChanged -= handler;
        }
    }
}