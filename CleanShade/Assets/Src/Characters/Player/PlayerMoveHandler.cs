using Src.Input;
using UnityEngine;
using Zenject;

namespace Src.Characters.Player
{
    public class PlayerMoveHandler : IFixedTickable
    {
        private readonly CharactersSettings.Player settings;
        private readonly PlayerModel player;
        private readonly PlayerState playerState;
        private readonly IPlayerInputState inputState;
        private readonly IsometricScalingSettings isometricScalingSettings;

        public PlayerMoveHandler(
            IPlayerInputState inputState,
            PlayerModel player,
            PlayerState playerState,
            CharactersSettings.Player settings,
            IsometricScalingSettings isometricScalingSettings)
        {
            this.settings = settings;
            this.player = player;
            this.playerState = playerState;
            this.inputState = inputState;
            this.isometricScalingSettings = isometricScalingSettings;
        }

        public void FixedTick()
        {
            if (playerState.IsDead)
            {
                return;
            }
 
            var moveVector = inputState.MoveVector;
            var calculatedMoveVector = moveVector.normalized * (settings.MoveSpeed * Time.deltaTime);
            // Applying scale as we use isometric projection on Z scale 
            player.RigidBody.position += calculatedMoveVector.ApplyIsometricScale(scale: isometricScalingSettings.IsometricScalingFactor);
            if (!calculatedMoveVector.x.IsAlmostZero())
            {
                player.SpineSkeletonAnimation.Skeleton.ScaleX =
                    calculatedMoveVector.x >= 0 ? -1 : 1;
            }
        }
    }
}