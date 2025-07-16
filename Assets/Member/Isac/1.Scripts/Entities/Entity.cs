using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Member.Isac._1.Scripts.Entities
{
    public class Entity : MonoBehaviour
    {
        public UnityEvent OnHitEvent;
        public UnityEvent OnDeathEvent;

        public bool IsDead { get; set; }
        protected Dictionary<Type, IEntityComponent> _components;

        public void EntityDestroy()
        {
            Destroy(gameObject);
        }
        
        protected virtual void Awake()
        {
            _components = new Dictionary<Type, IEntityComponent>();
            AddComponents();
            InitializeComponents();
            AfterInitialize();
        }
        
        protected virtual void AddComponents()
        {
            GetComponentsInChildren<IEntityComponent>().ToList()
                .ForEach(c => _components.Add(c.GetType(), c));
        }
        
        protected virtual void InitializeComponents()
        {
            _components.Values.ToList()
                .ForEach(c => c.Initialize(this));
        }
        
        private void AfterInitialize()
        {
            _components.Values.OfType<IAfterInit>().ToList()
                .ForEach(c => c.AfterInit());
        }

        public T GetCompo<T>(bool isDerived = false) where T : IEntityComponent
        { 
            if (_components.TryGetValue(typeof(T), out IEntityComponent component))
            {
                return (T)component;
            }

            if (isDerived != false)
            {
                Type findType = _components.Keys.FirstOrDefault(t => t.IsSubclassOf(typeof(T)));
                if (findType != null)
                    return (T)_components[findType];
            }

            return default;
        }

        public IEntityComponent GetCompo(Type type)
            => _components.GetValueOrDefault(type);
    }
}