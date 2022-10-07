using System.Collections;
using System.Collections.Generic;

public class StrenghtBuff : Buff
{
    private int _buffAmount;

    public StrenghtBuff(int buffAmount) : base()
    {
        _buffAmount = buffAmount;
    }

    public StrenghtBuff(float duration, int buffAmount) : base(duration)
    {
        _buffAmount = buffAmount;
    }

    public override void Grant(Character character)
    {
        character.Strenght += _buffAmount;
    }

    public override void Remove(Character character)
    {
        character.Strenght -= _buffAmount;
    }
}
