using Src.Input;
using UnityEngine;
using Zenject;

namespace Src.Characters.Player
{
    public class PlayerAimHandler : IFixedTickable
    {
        private readonly PlayerModel playerModel;
        private readonly IPlayerInputState playerInputState;
        
        public PlayerAimHandler(
            IPlayerInputState playerInputState,
            PlayerModel playerModel)
        {
            this.playerInputState = playerInputState;
            this.playerModel = playerModel;
        }
        
        public void FixedTick()
        {
            var aimLocation = playerInputState.AimLocation;
            aimLocation.y = playerModel.Position.y;
            playerModel.AimVector = aimLocation;

            HandleAimBowTransform(aimLocation);
        }

        private void HandleAimBowTransform(Vector3 aimLocation)
        {
            var aimTranformVector = aimLocation - playerModel.Position;
            aimTranformVector.y = aimTranformVector.z;
            aimTranformVector.z = 0f;

            playerModel.AimTransform.position = aimTranformVector;
            playerModel.SpineSkeletonAnimation.Skeleton.ScaleX = aimTranformVector.x >= 0 ? 1 : -1;
        }
    }
}