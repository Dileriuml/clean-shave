using Spine;
using Spine.Unity;
using Src.Utility.Container.Installers;
using Zenject;

namespace Src.Characters.Player
{
    public class PlayerAnimationHandler : ITickable, IInitializable
    {
        private const int MoveAnimationTrackId = 0;
        private const int FireAnimationTrackId = 1;
        
        private readonly CharactersSettings.Player playerSettings;
        private readonly AnimationsInstaller.PlayerAnimations playerAnimations;
        private readonly PlayerState playerState;
        private readonly SkeletonAnimation skeletonBody;
        
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
        
        public void Initialize()
        {
            SetFireAnimation();
        }

        private float FireTimeScal => playerAnimations.Fire.DefaultTimeScale * playerSettings.FireRate;
        
        private TrackEntry ArrowFiringAnimationTrack => skeletonBody.state.GetCurrent(FireAnimationTrackId);

        private void HandleMoveAnimation()
        {
            var moveAnimation = playerState.IsMoving ? playerAnimations.Move : playerAnimations.Idle;
            SetMoveTrackAnimation(moveAnimation.Asset, true, moveAnimation.DefaultTimeScale);
        }

        private void HandleFireAnimation()
        {
            if (ArrowFiringAnimationTrack is null)
            {
                SetFireAnimation();
            }

            if (playerState.IsFiring)
            {
                ActivateFireAimAnimation();
            }
            else
            {
                CancelFireAnimation();
            }
        }

        private void SetMoveTrackAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
        {
            if (currentAnimation == animation)
            {
                return;
            }

            currentAnimation = animation;
            skeletonBody.state.SetAnimation(MoveAnimationTrackId, animation, loop).TimeScale = timeScale;
        }
        
        private void CancelFireAnimation()
        {
            if (ArrowFiringAnimationTrack is not { Loop: true })
            {
                return;
            }

            var currentTime = ArrowFiringAnimationTrack.AnimationTime % ArrowFiringAnimationTrack.Animation.Duration;
            ArrowFiringAnimationTrack.Loop = false;
            ArrowFiringAnimationTrack.TrackTime = currentTime;
            SetFireAnimationScale(true);
        }

        private void ActivateFireAimAnimation()
        {
            if (ArrowFiringAnimationTrack is not { Loop: false })
            {
                return;
            }
            
            var currentTime = ArrowFiringAnimationTrack.AnimationTime % ArrowFiringAnimationTrack.Animation.Duration;
            ArrowFiringAnimationTrack.Loop = true;
            ArrowFiringAnimationTrack.TrackTime = currentTime;
            SetFireAnimationScale(false);
        }

        private void SetFireAnimation(bool withLoop = true)
        {
            skeletonBody.state.SetAnimation(FireAnimationTrackId, playerAnimations.Fire.Asset, withLoop);
            ArrowFiringAnimationTrack.TimeScale = 0;
        }
        
        private void SetFireAnimationScale(bool invert)
        {
            ArrowFiringAnimationTrack.TimeScale = (invert ? -1 : 1) * FireTimeScal;
        }
    }
}