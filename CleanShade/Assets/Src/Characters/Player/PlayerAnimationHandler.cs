using Spine.Unity;
using Src.Utility.Container.Installers;
using Zenject;

namespace Src.Characters.Player
{
    public class PlayerAnimationHandler : ITickable
    {
        private readonly AnimationsInstaller.PlayerAnimations playerAnimations;
        private readonly PlayerState playerState;
        private readonly SkeletonAnimation animation;

        private AnimationReferenceAsset currentAnimation;
        
        public PlayerAnimationHandler(
            PlayerState playerState, 
            PlayerModel playerModel, 
            AnimationsInstaller.PlayerAnimations playerAnimations)
        {
            this.playerState = playerState;
            this.animation = playerModel.SpineSkeletonAnimation;
            this.playerAnimations = playerAnimations;
        }

        public void Tick()
        {
            var moveAnimation = playerState.IsMoving ? playerAnimations.Move : playerAnimations.Idle;
            SetAnimation(moveAnimation, true, 1f);
        }
        
        private void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
        {
            if (currentAnimation == animation)
            {
                return;
            }

            currentAnimation = animation;
            this.animation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        }
    }
}