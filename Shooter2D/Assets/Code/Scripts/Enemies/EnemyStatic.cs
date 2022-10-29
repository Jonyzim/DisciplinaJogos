namespace MWP.Enemies
{
    public class EnemyStatic : Enemy
    {
        // [SerializeField] private Vector3 _speed = new Vector3(0.1f, 0, 0);
        // [SerializeField] private bool _randomSpeed = false;

        // //Unity Methods
        // protected void OnTriggerEnter2D(Collider2D collision)
        // {
        //     // base.OnTriggerEnter2D(collision);
        //     if (collision.CompareTag("Wall"))
        //     {
        //         _speed = -_speed;
        //     }
        // }

        // protected override void Start()
        // {
        //     base.Start();
        //     if (_randomSpeed)
        //     {
        //         float _x = Random.Range(-0.02f, 0.02f);
        //         float _y = Random.Range(-0.02f, 0.02f);
        //         _speed = new Vector3(_x, _y, 0);
        //     }
        // }

        public override void Attack()
        {
        }
    }
}