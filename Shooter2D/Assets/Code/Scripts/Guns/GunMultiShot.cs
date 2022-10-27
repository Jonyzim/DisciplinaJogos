using MWP.Guns.Bullets;
using System.Collections;
using UnityEngine;

namespace MWP.Guns
{
    public abstract class GunMultiShot : Gun
    {
        [Header("ShotgunSpecifics")]
        [SerializeField] private int _bulletsSpawned;
        [SerializeField] private float _bulletLag;

        private bool _fired = false;

        public override Bullet Fire(Vector2 direction, int strenght, float aim)
        {
            if (Cd <= 0 && _curClip > 0 && !_fired)
            {
                _fired = true;
                FMODUnity.RuntimeManager.PlayOneShot(ShotSfxEvent, transform.position);
                _curClip -= 1;

                for (int i = 0; i < _bulletsSpawned; i++)
                {
                    StartCoroutine(FireLag(direction, strenght, aim, Random.Range(-_bulletLag, _bulletLag)));
                }
            }

            return null;
        }

        private IEnumerator FireLag(Vector2 direction, int strenght, float aim, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            base.Fire(direction, strenght, aim);
        }

        public override void ReleaseFire()
        {
            _fired = false;
        }

    }
}