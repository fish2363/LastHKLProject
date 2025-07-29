using UnityEngine;
using UnityEngine.Events;

namespace Blade.Entities
{
    public class EntityAnimator : MonoBehaviour, IEntityComponent
    {
        public UnityEvent<Vector3,Quaternion> OnAnimatorMoveEvent;
        private Animator _animator;
        
        public bool ApplyRootMotion
        {
            get => _animator.applyRootMotion;
            set => _animator.applyRootMotion = value;
        }

        private Entity _entity;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _animator = GetComponent<Animator>();
        }

        private void OnAnimatorMove()
        {
            //Apply Root motion 에 의해서 transform이 움직일때 호출됨
            OnAnimatorMoveEvent?.Invoke(_animator.deltaPosition,_animator.deltaRotation);
        }

        public void SetParam(int hash, float value) => _animator.SetFloat(hash, value);
        public void SetParam(int hash, bool value) => _animator.SetBool(hash, value);
        public void SetParam(int hash, int value) => _animator.SetInteger(hash, value);
        public void SetParam(int hash) => _animator.SetTrigger(hash);

        public void SetParam(int hash,float value,float dampTime)
            => _animator.SetFloat(hash,value,dampTime,Time.deltaTime);

        public void SetAnimatorOff()
        {
            _animator.enabled = false;
        }
    }
}