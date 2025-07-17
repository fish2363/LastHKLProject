using UnityEngine;

namespace Member.Isac._1.Scripts.Combat
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class AttackDataSO : ScriptableObject
    {
        public string attackName;
        public float movementPower;
        public float damageMultiplier;
        public float damageIncrease;
        public bool isPowerAttack;
        public float impulseForce;
        public float knockBackForce;
        public float knockBackDuration;

        private void OnEnable()
        {
            attackName = this.name;
        }
    }
}