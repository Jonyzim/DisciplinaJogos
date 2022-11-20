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

        public override void Grant()
        {
            Owner.health += _hpAmount;
            Owner.UpdateHealth(_hpAmount);

            Owner.strength += _strAmount;
            Owner.aim += _aimAmount;
            Owner.speed += _spdAmount;
        }

        public override void Remove()
        {
            Owner.health -= _hpAmount;
            Owner.UpdateHealth();

            Owner.strength -= _strAmount;
            Owner.aim -= _aimAmount;
            Owner.speed -= _spdAmount;
        }
    }
}