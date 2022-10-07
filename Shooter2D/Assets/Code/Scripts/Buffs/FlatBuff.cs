using System.Collections;
using System.Collections.Generic;

public class FlatBuff : Buff
{
    private int _hpAmount;
    private int _strAmount;
    private int _aimAmount;
    private int _spdAmount;

    public FlatBuff(int hpAmount = 0, int strAmount = 0, int hitAmount = 0, int spdAmount = 0) : base()
    {
        _hpAmount = hpAmount;
        _strAmount = strAmount;
        _aimAmount = hitAmount;
        _spdAmount = spdAmount;
    }

    public FlatBuff(float duration, int hpAmount = 0, int strAmount = 0, int hitAmount = 0, int spdAmount = 0) : base(duration)
    {
        _hpAmount = hpAmount;
        _strAmount = strAmount;
        _aimAmount = hitAmount;
        _spdAmount = spdAmount;
    }

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
