using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected Enemy Context;
    protected EnemyStateFactory Factory;

    public abstract void StartState();
    public abstract void UpdateState();
    public abstract void ExitState();

    public EnemyState(Enemy context, EnemyStateFactory factory)
    {
        Context = context;
        Factory = factory;
    }
}
