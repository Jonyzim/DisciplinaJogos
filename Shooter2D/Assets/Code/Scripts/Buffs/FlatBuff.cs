using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlatBuff", menuName = "Shooter2D/Buffs/FlatBuff", order = 0)]
public class FlatBuff : Buff
{
    [Header("Effects")]
    [SerializeField]
    private int _hpAmount;

    [SerializeField]
    private int _strAmount;

    [SerializeField]
    private int _aimAmount;

    [SerializeField]
    private int _spdAmount;

    public override void Grant(Character character)
    {
        character.Health += _hpAmount;
        character.UpdateHealth(_hpAmount);

        character.Strenght += _strAmount;
        character.Aim += _aimAmount;
        character.Speed += _spdAmount;
    }

    public override void Remove(Character character)
    {
        character.Health -= _hpAmount;
        character.UpdateHealth();

        character.Strenght -= _strAmount;
        character.Aim -= _aimAmount;
        character.Speed -= _spdAmount;
    }
}
