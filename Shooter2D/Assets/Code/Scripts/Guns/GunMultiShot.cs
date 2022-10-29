using System.Collections;
using FMODUnity;
using MWP.Guns.Bullets;
using UnityEngine;
using UnityEngine.Serialization;

namespace MWP.Guns
{
    public abstract class GunMultiShot : Gun
    {
        [FormerlySerializedAs("_bulletsSpawned")] [Header("ShotgunSpecifics")] [SerializeField]
        private int bulletsSpawned;

        [FormerlySerializedAs("_bulletLag")] [SerializeField] private float bulletLag;

        private bool _fired;

        public override Bullet Fire(Vector2 direction, int strength, float aim)
        {
            if (Cd <= 0 && _curClip > 0 && !_fired)
            {
                _fired = true;
                RuntimeManager.PlayOneShot(ShotSfxEvent, transform.position);
                _curClip -= 1;

                for (var i = 0; i < bulletsSpawned; i++)
                    StartCoroutine(FireLag(direction, strength, aim, Random.Range(-bulletLag, bulletLag)));
            }

            return null;
        }

        private IEnumerator FireLag(Vector2 direction, int strength, float aim, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            base.Fire(direction, strength, aim);
        }

        public override void ReleaseFire()
        {
            _fired = false;
        }
    }
}