using Member.Isac._1.Scripts.Entities;
using UnityEngine;
using UnityEngine.Events;

namespace Blade.Entities
{
    public class EntityAnimator : MonoBehaviour, IEntityComponent
    {
        public UnityEvent<Vector3,Quaternion> OnAnimatorMoveEvent;
        [SerializeField] private Animator animator;
        
        public bool ApplyRootMotion
        {
            get => animator.applyRootMotion;
            set => animator.applyRootMotion = value;
        }

        private Entity _entity;

        public void Initialize(Entity entity)
        {
            _entity = entity;
        }

        private void OnAnimatorMove()
        {
            //Apply Root motion �� ���ؼ� transform�� �����϶� ȣ���
            OnAnimatorMoveEvent?.Invoke(animator.deltaPosition,animator.deltaRotation);
        }

        public void SetParam(int hash, float value) => animator.SetFloat(hash, value);
        public void SetParam(int hash, bool value) => animator.SetBool(hash, value);
        public void SetParam(int hash, int value) => animator.SetInteger(hash, value);
        public void SetParam(int hash) => animator.SetTrigger(hash);

        public void SetParam(int hash,float value,float dampTime)
            => animator.SetFloat(hash,value,dampTime,Time.deltaTime);

        public void SetAnimatorOff()
        {
            animator.enabled = false;
        }
    }
}