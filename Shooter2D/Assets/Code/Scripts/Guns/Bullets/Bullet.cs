using MWP.Enemies;
using MWP.Misc;
using UnityEngine;

namespace MWP.Guns.Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] protected float Speed;
        [SerializeField] protected float Lifetime;
        [SerializeField] protected int OwnerId;
        [SerializeField] private GameObject _destroyFxPrefab;
        protected float DamageCaused;
        protected Vector3 Direction;

        //Unity Methods
        protected virtual void Start()
        {
            DestroyBullet(Lifetime);
        }

        //Methods
        public void SetPlayer(int id)
        {
            OwnerId = id;
        }

        public virtual void SetVariables(Vector2 direction, int strength, int damage)
        {
            Direction = direction;
            DamageCaused = damage * ((float)strength / 100);
        }

        protected virtual void DestroyBullet(float timer = 0)
        {
            Instantiate(_destroyFxPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject, timer);
        }

        protected virtual void AddPlayerScore(int n)
        {
            if (OwnerId != -1)
                GameEvents.Instance.ScoreUpdate(OwnerId, n);
        }

        protected virtual void DamageOnEnemy(Enemy enemy, Vector3? pos)
        {
            var life = enemy.Life;
            enemy.TakeDamage(pos, (int)DamageCaused);
            AddPlayerScore(Mathf.Min(life, (int)DamageCaused));
        }


        protected virtual void DamageOnEnemy(Enemy enemy, Vector3? pos, float damage)
        {
            var life = enemy.Life;
            enemy.TakeDamage(pos, (int)damage);
            AddPlayerScore(Mathf.Min(life, (int)damage));
        }
    }
}