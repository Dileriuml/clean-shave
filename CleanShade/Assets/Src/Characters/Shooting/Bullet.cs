using Src.Characters.Player;
using UnityEngine;
using Zenject;

namespace Src.Characters.Shooting
{
    public class Bullet : MonoBehaviour, IPoolable<float, float, IsometricScalingSettings, BulletOwnerType, IMemoryPool>
    {
        private float spawnedTime;
        private float speed;
        private float lifeTime;
        private BulletOwnerType ownerType;

        [SerializeField]
        private Transform childTransform;

        private IMemoryPool pool;
        private float isometricScaleModificator;
        private IsometricScalingSettings isometricScalingSettings;

        public Transform ChildTransform => childTransform;
        
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
            transform.position += moveChange.ApplyIsometricScale(isometricScalingSettings.IsometricScalingFactor);

            if (Time.realtimeSinceStartup - spawnedTime > lifeTime)
            {
                pool?.Despawn(this);
            }
        }

        public void OnSpawned(float speed, float lifeTime, IsometricScalingSettings isometricScaleSettings, BulletOwnerType type, IMemoryPool pool)
        {
            isometricScalingSettings = isometricScaleSettings;
            this.pool = pool;
            ownerType = type;
            this.speed = speed;
            this.lifeTime = lifeTime;

            //renderer.material = type == BulletOwnerType.FromEnemy ? enemyMaterial : playerMaterial;

            spawnedTime = Time.realtimeSinceStartup;
        }

        public void OnDespawned()
        {
            pool = null;
        }

        public class Factory : PlaceholderFactory<float, float,IsometricScalingSettings, BulletOwnerType, Bullet>
        {
        }
    }
}