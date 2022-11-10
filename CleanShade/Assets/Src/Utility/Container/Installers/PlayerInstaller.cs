using System;
using Src.CameraHandling;
using Src.Characters;
using Src.Characters.Player;
using Src.Input;
using UnityEngine;
using Zenject;

namespace Src.Utility.Container.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private Settings settings = null;

        public override void InstallBindings()
        {
            Container.Bind<PlayerInputActions>().AsSingle();
            Container.Bind<CharactersSettings>().AsSingle();
            Container.Bind<PlayerModel>().AsSingle()
                     .WithArguments(settings.MeshRenderer, settings.Rigidbody);

            Container.BindInterfacesTo<PlayerInputState>().AsSingle();
            Container.BindInterfacesTo<CameraFollowHandler>().AsSingle();
            Container.BindInterfacesTo<PlayerInputHandler>().AsSingle();
            Container.BindInterfacesTo<PlayerMoveHandler>().AsSingle();
            //Container.BindInterfacesAndSelfTo<PlayerDamageHandler>().AsSingle();
            //Container.BindInterfacesTo<PlayerDirectionHandler>().AsSingle();
            //Container.BindInterfacesTo<PlayerShootHandler>().AsSingle();

            //Container.BindInterfacesTo<PlayerHealthWatcher>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public Rigidbody Rigidbody;
            public MeshRenderer MeshRenderer;
        }
    }
}