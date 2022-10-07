using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GiveBuffTemp : MonoBehaviour
{
    public Character CharRef;
    public Buff buff;

    [Button]
    public void GiveBuff()
    {
        CharRef.AddBuff(Instantiate(buff));
    }

}
