using System;
using UnityEngine;

namespace MWP.Misc
{
    public class AutoDestroy : MonoBehaviour
    {
        [SerializeField]
        private float timeToDestroy;

        [SerializeField]
        private bool playAtStart;

        private void Start()
        {
            if (!playAtStart) return;
            DestroyThis();
        }

        private void DestroyThis()
        {
            Destroy(gameObject, timeToDestroy);
        }
    }
}