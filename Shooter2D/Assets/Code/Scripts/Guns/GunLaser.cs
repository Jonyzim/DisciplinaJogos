
using System;
using MWP.Guns.Bullets;
using UnityEngine;
using FMODUnity;
using UnityEngine.PlayerLoop;

namespace MWP.Guns
{
    public class GunLaser : GunAutomatic
    {
        [SerializeField] private LineRenderer _line;
        [SerializeField] private StudioEventEmitter eventEmitter;

        protected override void Start()
        {
            base.Start();
            _line.gameObject.SetActive(false);
        }
        
        protected override void Update()
        {
            base.Update();
            _line.SetPosition(0, SpawnTransf.position);
        }

        public override Bullet Fire(Vector2 direction, int strength, float aim)
        {
            if (!(Cd <= 0) || _curClip <= 0) return null;
            _curClip -= 1;
            _line.gameObject.SetActive(true);
            
            //_line.SetPosition(1, SpawnTransf.position + (Vector3)direction);

            return base.Fire(direction, strength, aim);

        }
        public void SetLinePos(int id, Vector3 pos)
        {
            _line.SetPosition(id, pos);
        }

        public override void ReleaseFire()
        {
            if (eventEmitter.IsPlaying())
                eventEmitter.Stop();
            _line.gameObject.SetActive(false);
        }
        protected override void FireProps()
        {
            if(!eventEmitter.IsPlaying())
                eventEmitter.Play();
        }
        protected override void ReloadProps(float time)
        {
            
        }
    }
}