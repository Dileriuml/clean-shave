using Src.Characters.Player;
using UnityEngine;
using Zenject;

namespace Src.Characters.Shooting
{
    public class Bullet : MonoBehaviour, IPoolable<float, float, BulletOwnerType, IMemoryPool>
    {
        private float startTime;
        private float speed;
        private float lifeTime;
        private BulletOwnerType ownerType;

        [SerializeField]
        private MeshRenderer renderer = null;

        [SerializeField]
        private Material playerMaterial = null;

        [SerializeField]
        private Material enemyMaterial = null;

        private IMemoryPool pool;

        public Vector3 MoveDirection => transform.forward;

        public void OnTriggerEnter(Collider other)
        {
            // var enemyView = other.GetComponent<EnemyView>();
            //
            // if (enemyView != null && _type == BulletTypes.FromPlayer)
            // {
            //     enemyView.Facade.Die();
            //     _pool.Despawn(this);
            // }
            // else
            
            var player = other.GetComponent<PlayerFacade>();

            if (player != null && ownerType == BulletOwnerType.FromEnemy)
            {
                player.TakeDamage(MoveDirection);
                pool.Despawn(this);
            }
        }

        public void Update()
        {
            var moveChange = MoveDirection * speed * Time.deltaTime;
            transform.position += moveChange;

            if (Time.realtimeSinceStartup - startTime > lifeTime)
            {
                pool?.Despawn(this);
            }
        }

        public void OnSpawned(float speed, float lifeTime, BulletOwnerType type, IMemoryPool pool)
        {
            this.pool = pool;
            ownerType = type;
            this.speed = speed;
            this.lifeTime = lifeTime;

            //renderer.material = type == BulletOwnerType.FromEnemy ? enemyMaterial : playerMaterial;

            startTime = Time.realtimeSinceStartup;
        }

        public void OnDespawned()
        {
            pool = null;
        }

        public class Factory : PlaceholderFactory<float, float, BulletOwnerType, Bullet>
        {
        }
    }
}