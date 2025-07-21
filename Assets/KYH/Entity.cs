using Member.Isac._1.Scripts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


    public class Entity : MonoBehaviour
    {
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
            AfterInitializeComponents();
        }


        protected virtual void AddComponents()
        {
            GetComponentsInChildren<IEntityComponent>().ToList()
                .ForEach(component => _components.Add(component.GetType(), component));
        }

        protected virtual void InitializeComponents()
        {
            _components.Values.ToList().ForEach(component => component.Initialize(this));
        }

        private void AfterInitializeComponents()
        {
            _components.Values.OfType<IAfterInit>()
                .ToList().ForEach(component => component.AfterInit());
        }

        public T GetCompo<T>() where T : IEntityComponent
            => (T)_components.GetValueOrDefault(typeof(T));

        public IEntityComponent GetCompo(Type type)
            => _components.GetValueOrDefault(type);
    }
