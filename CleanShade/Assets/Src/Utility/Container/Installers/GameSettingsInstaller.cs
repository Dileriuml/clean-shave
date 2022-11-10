using System;
using Src.CameraHandling;
using Src.Characters;
using Src.Characters.Player;
using Src.Characters.Shooting;
using UnityEngine;
using Zenject;

namespace Src.Utility.Container.Installers
{
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public CharactersSettings Characters;
        public CameraSettings Camera;
        public PrefabSettings Prefabs;
        public PlayerShootHandler.Settings PlayerShootSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(Characters.PlayerSettings).IfNotBound();
            Container.BindInstance(Characters.EnemySettings).IfNotBound();
            Container.BindInstance(PlayerShootSettings).IfNotBound();
            Container.BindInstance(Camera).IfNotBound();
            
            RegisterFactories();
        }

        private void RegisterFactories()
        {
            Container.BindFactory<float, float, BulletOwnerType, Bullet, Bullet.Factory>()
                .FromPoolableMemoryPool<float, float, BulletOwnerType, Bullet, BulletPool>(poolBinder => poolBinder
                    .WithInitialSize(20)
                    .FromComponentInNewPrefab(Prefabs.BulletPrefab)
                    .UnderTransformGroup("Bullets"));
        }
        
        [Serializable]
        public class PrefabSettings
        {
            public GameObject BulletPrefab;
        }
        
        public class BulletPool : MonoPoolableMemoryPool<float, float, BulletOwnerType, IMemoryPool, Bullet>
        {
        }
    }
}