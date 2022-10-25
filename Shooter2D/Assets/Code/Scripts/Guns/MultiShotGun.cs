using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiShotGun : Gun
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
            ShotSFX.Play();
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

    protected override void FireProps()
    {

    }

    public override void ReleaseFire()
    {
        _fired = false;
    }

}
