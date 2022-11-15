using System;
using Spine.Unity;
using Src.Characters.Anim;
using Zenject;

namespace Src.Utility.Container.Installers
{
    public class AnimationsInstaller : ScriptableObjectInstaller<AnimationsInstaller>
    {
        public PlayerAnimations PlayerAnimationConfig;
        public EnemyAnimations EnemyAnimationConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(PlayerAnimationConfig).IfNotBound();
            Container.BindInstance(EnemyAnimationConfig).IfNotBound();
        }

        [Serializable]
        public class PlayerAnimations
        {
            public AnimationStateConfig Idle;
            public AnimationStateConfig Move;
            public AnimationStateConfig Fire;
        }
        
        [Serializable]
        public class EnemyAnimations
        {
            public AnimationStateConfig Idle;
            public AnimationStateConfig Move;
            public AnimationStateConfig Fire;
        }
    }
}