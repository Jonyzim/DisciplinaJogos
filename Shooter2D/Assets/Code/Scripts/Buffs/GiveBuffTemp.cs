using NaughtyAttributes;
using UnityEngine;

namespace MWP.Buffs
{
    public class GiveBuffTemp : MonoBehaviour
    {
        public Character.Character CharRef;
        public Buff buff;

        [Button]
        public void GiveBuff()
        {
            CharRef.AddBuff(Instantiate(buff));
        }
    }
}