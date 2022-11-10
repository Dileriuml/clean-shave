using System;
using Spine.Unity;
using Zenject;

namespace Src.Utility.Container.Installers
{
    public class AnimationsInstaller : ScriptableObjectInstaller<AnimationsInstaller>
    {
        public PlayerAnimations PlayerStates;

        public override void InstallBindings()
        {
            Container.BindInstance(PlayerStates).IfNotBound();
        }

        [Serializable]
        public class PlayerAnimations
        {
            public AnimationReferenceAsset Idle;
            public AnimationReferenceAsset Move;
            public AnimationReferenceAsset Fire;
        }
    }
}