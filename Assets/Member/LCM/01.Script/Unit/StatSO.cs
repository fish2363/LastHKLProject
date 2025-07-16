using System;
using System.Collections.Generic;
using UnityEngine;

namespace Member.LCM._01.Script.Unit
{
    [CreateAssetMenu(fileName = "Stat", menuName = "SO/StatSystem/Stat", order = 0)]
    public class StatSO : ScriptableObject, ICloneable
    {
        public delegate void ValueChangeHandler(StatSO stat, float currentValue, float previousValue);
        public event ValueChangeHandler OnValueChanged;
        
        public string statName;
        [SerializeField] private float statValue;

        private readonly Dictionary<object, float> _modifyValueByKey = new Dictionary<object, float>();
        

        private float _modifiedValue = 0;

        
        public float Value => statValue + _modifiedValue;

        public float StatValue
        {
            get => statValue;
            set
            {
                float prevValue = Value;
                statValue = value;
                TryInvokeValueChangeEvent(Value, prevValue);
            }
        }

        private void TryInvokeValueChangeEvent(float value, float prevValue)
        {
            if (Mathf.Approximately(value, prevValue) == false)
            {
                OnValueChanged?.Invoke(this, value, prevValue);
            }
        }
        
        public void AddModifier(object key, float value)
        {
            if (_modifyValueByKey.ContainsKey(key)) return; //동일아이템에 대한 중복적용은 허용하지 않는다.

            float prevValue = Value;
            _modifiedValue += value;
            _modifyValueByKey.Add(key, value);
            TryInvokeValueChangeEvent(Value, prevValue);
        }

        public void RemoveModifier(object key)
        {
            if (_modifyValueByKey.TryGetValue(key, out float value))
            {
                float prevValue = Value;
                _modifiedValue -= value;
                _modifyValueByKey.Remove(key);
                
                TryInvokeValueChangeEvent(Value, prevValue);
            }
        }

        public void ClearModifier()
        {
            float prevValue = Value;
            _modifyValueByKey.Clear();
            _modifiedValue = 0;
            TryInvokeValueChangeEvent(Value, prevValue);
        }


        public object Clone()
        {
            return Instantiate(this); //자기자신을 복제해서 준다.
        }
    }
}
