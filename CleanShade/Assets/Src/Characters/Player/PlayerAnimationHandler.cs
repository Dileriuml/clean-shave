using Spine;
using Spine.Unity;
using Src.Utility.Container.Installers;
using UnityEngine;
using Zenject;

namespace Src.Characters.Player
{
    public class PlayerAnimationHandler : ITickable
    {
        private const int MoveAnimationTrackId = 0;
        private const int FireAnimationTrackId = 1;
        
        private readonly AnimationsInstaller.PlayerAnimations playerAnimations;
        private readonly PlayerState playerState;
        private readonly SkeletonAnimation animation;
        
        private bool isFireAnimationPlaying;
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
            HandleMoveAnimation();
            HandleFireAnimation();
        }
        
        private TrackEntry ArrowFiringAnimationTrack => animation.state.GetCurrent(FireAnimationTrackId);

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
        }

        private void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
        {
            if (currentAnimation == animation)
            {
                return;
            }

            currentAnimation = animation;
            
            this.animation.state.SetAnimation(MoveAnimationTrackId, animation, loop).TimeScale = timeScale;
        }
        
        private void CancelFireAnimation()
        {
            if (ArrowFiringAnimationTrack == null)
            {
                Debug.Log($"No anim track to cancel");
                return;
            }

            ArrowFiringAnimationTrack.Loop = false;
            ArrowFiringAnimationTrack.Reverse = true;
            Debug.Log($"There is track removing loop");
        }
        
        private void SetFireAnimation()
        {
            if (ArrowFiringAnimationTrack is {} fat)
            {
                Debug.Log($"Track resumed");
                fat.Reverse = false;
                fat.Loop = true;
                return;
            }
            
            Debug.Log($"No track - setting track");
            animation.state.SetAnimation(FireAnimationTrackId, playerAnimations.Fire.Asset, true).TimeScale = playerAnimations.Fire.DefaultTimeScale;
        }
    }
}