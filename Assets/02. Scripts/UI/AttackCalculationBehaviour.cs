using UnityEngine;
using UnityEngine.Events;
using Isekai.Battle;

namespace Isekai.Characters
{
    [System.Serializable]
    public struct AttackData // TODO: probably more fields
    {
        public int physicalDamage;
    }

    public class AttackCalculationBehaviour : MonoBehaviour, ICharacterInitializable
    {
        private CombatData combatDataRef;

        private UnityAction<AttackData> onAttack;

        public void OnAttackPerformed(float motionValue)
        {
            AttackData ad = new AttackData();
            ad.physicalDamage = (int) Mathf.Floor((float)combatDataRef.Damage * motionValue);
            onAttack?.Invoke(ad);
        }

        public void RegisterOnAttackTarget(UnityAction<AttackData> action)
        {
            onAttack = action;
        }

        public void UnregisterOnAttackTarget()
        {
            onAttack = null;
        }

        public void InitializeCharacter(CharacterData cd)
        {
            switch (cd.CharacterType)
            {
                case CharacterTypeEnum.Goddess:
                case CharacterTypeEnum.PassiveNPC:
                    UnityEngine.Assertions.Assert.IsTrue(false);
                    GamedevsToolbox.Utils.Logger.Logger.LogError("Attack Calculation Behaviour attached to a non attacking character type");
                    break;
            }
            combatDataRef = cd.CombatData;
        }
    }
}