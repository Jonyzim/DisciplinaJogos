using UnityEngine;

namespace MWP.Buffs
{
    [CreateAssetMenu(fileName = "FlatBuff", menuName = "Shooter2D/Buffs/FlatBuff", order = 0)]
    public class FlatBuff : Buff
    {
        [Header("Effects")] [SerializeField] private int _hpAmount;

        [SerializeField] private int _strAmount;

        [SerializeField] private int _aimAmount;

        [SerializeField] private int _spdAmount;

        public override void Grant(Character character)
        {
            character.health += _hpAmount;
            character.UpdateHealth(_hpAmount);

            character.strength += _strAmount;
            character.aim += _aimAmount;
            character.speed += _spdAmount;
        }

        public override void Remove(Character character)
        {
            character.health -= _hpAmount;
            character.UpdateHealth();

            character.strength -= _strAmount;
            character.aim -= _aimAmount;
            character.speed -= _spdAmount;
        }
    }
}