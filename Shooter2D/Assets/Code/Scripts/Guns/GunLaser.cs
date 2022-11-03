
using MWP.Guns.Bullets;
using UnityEngine;
namespace MWP.Guns
{
    public class GunLaser : GunAutomatic
    {
        [SerializeField] private LineRenderer _line;
        public override Bullet Fire(Vector2 direction, int strength, float aim)
        {
            if (!(Cd <= 0) || _curClip <= 0) return null;
            _curClip -= 1;
            _line.gameObject.SetActive(true);
            _line.SetPosition(0,SpawnTransf.position);
            //_line.SetPosition(1, SpawnTransf.position + (Vector3)direction);

            return base.Fire(direction, strength, aim);

        }
        public void SetLinePos(int id,Vector3 pos)
        {
            _line.SetPosition(id, pos);
        }

        public override void ReleaseFire()
        {
            _line.gameObject.SetActive(false);
        }
        protected override void ReloadProps(float time)
        {
            
        }
    }
}