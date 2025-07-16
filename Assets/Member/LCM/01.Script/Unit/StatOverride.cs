using System;
using UnityEngine;

namespace Member.LCM._01.Script.Unit
{
    [Serializable]
    public class StatOverride
    {
        [Tooltip("유닛이 가질 스탯 (ex: HP, Damage)")]
        [SerializeField] private StatSO stat;
        
        [Tooltip("유닛이 가지는 기본 고유 스탯값")]
        [SerializeField] private float overrideBaseValue;

        public StatSO Stat => stat;
        public StatOverride(StatSO stat) => this.stat = stat;

        public StatSO CreateStat() //스탯 복제후 오버라이드 값을 넣어서 리턴해준다.
        {
            StatSO newStat = stat.Clone() as StatSO;
            Debug.Assert(newStat != null, $"{nameof(newStat)} stat clone failed");
            newStat.StatValue = overrideBaseValue;

            return newStat;
        }
    }
}