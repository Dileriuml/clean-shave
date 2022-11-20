using Spine;
using Spine.Unity;
using Src.Utility.Container.Installers;
using Zenject;

namespace Src.Characters.Player
{
    public class PlayerAnimationHandler : ITickable
    {
        private const int MoveAnimationTrackId = 0;
        private const int FireAnimationTrackId = 1;
        
        private readonly CharactersSettings.Player playerSettings;
        private readonly AnimationsInstaller.PlayerAnimations playerAnimations;
        private readonly PlayerState playerState;
        private readonly SkeletonAnimation skeletonBody;
        
        private bool isFireAnimationPlaying;
        private AnimationReferenceAsset currentAnimation;
        
        public PlayerAnimationHandler(
            PlayerState playerState, 
            PlayerModel playerModel, 
            CharactersSettings.Player playerSettings,
            AnimationsInstaller.PlayerAnimations playerAnimations)
        {
            this.playerState = playerState;
            this.skeletonBody = playerModel.SpineSkeletonAnimation;
            this.playerAnimations = playerAnimations;
            this.playerSettings = playerSettings;
        }
        
        public void Tick()
        {
            HandleMoveAnimation();
            HandleFireAnimation();
        }

        private float FireTimeScal => playerAnimations.Fire.DefaultTimeScale * playerSettings.FireRate;
        
        private TrackEntry ArrowFiringAnimationTrack => skeletonBody.state.GetCurrent(FireAnimationTrackId);

        private void HandleMoveAnimation()
        {
            var moveAnimation = playerState.IsMoving ? playerAnimations.Move : playerAnimations.Idle;
            SetAnimation(moveAnimation.Asset, true, moveAnimation.DefaultTimeScale);
        }

        private void HandleFireAnimation()
        {
            if (playerState.IsFiring)
            {
                SetFireAnimation();
            }
            else
            {
                CancelFireAnimation();
            }

            SetFireAnimationScale();
        }

        private void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
        {
            if (currentAnimation == animation)
            {
                return;
            }

            currentAnimation = animation;
            
            this.skeletonBody.state.SetAnimation(MoveAnimationTrackId, animation, loop).TimeScale = timeScale;
        }
        
        private void CancelFireAnimation()
        {
            if (ArrowFiringAnimationTrack == null)
            {
                return;
            }

            ArrowFiringAnimationTrack.Loop = false;
            
        }
        
        private void SetFireAnimation()
        {
            if (ArrowFiringAnimationTrack is {} fat)
            {
                fat.Loop = true;
                return;
            }
            
            skeletonBody.state.SetAnimation(FireAnimationTrackId, playerAnimations.Fire.Asset, true);
        }
        
        private void SetFireAnimationScale()
        {
            if (ArrowFiringAnimationTrack == null)
            {
                return;
            }

            ArrowFiringAnimationTrack.TimeScale = FireTimeScal;
        }
    }
}