using System;
using Spine.Unity;
using Src.Audio;
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
            Container.Bind<CharactersSettings>().AsSingle();
            Container.Bind<PlayerModel>().AsSingle()
                     .WithArguments(
                         settings.MeshRenderer, 
                         settings.Rigidbody, 
                         settings.SpineAnimator);

            Container.Bind<PlayerInputActions>().AsSingle();
            Container.BindInterfacesTo<PlayerInputState>().AsSingle();
            Container.BindInterfacesTo<AudioPlayer>().AsSingle();
            
            Container.BindInterfacesTo<PlayerInputHandler>().AsSingle();
            Container.BindInterfacesTo<PlayerMoveHandler>().AsSingle();
            Container.BindInterfacesTo<PlayerShootHandler>().AsSingle();
            Container.BindInterfacesTo<PlayerAimHandler>().AsSingle();
            
            Container.BindInterfacesTo<CameraFollowHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerAnimationHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerStateProvider>().AsSingle();
            
            Container.Bind<PlayerState>().AsSingle();
            //Container.BindInterfacesAndSelfTo<PlayerDamageHandler>().AsSingle();
            //Container.BindInterfacesTo<PlayerDirectionHandler>().AsSingle();

            //Container.BindInterfacesTo<PlayerHealthWatcher>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public Rigidbody Rigidbody;
            public MeshRenderer MeshRenderer;
            public SkeletonAnimation SpineAnimator;
        }
    }
}