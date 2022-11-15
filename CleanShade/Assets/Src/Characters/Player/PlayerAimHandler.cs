using Src.Input;
using Zenject;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Src.Characters.Player
{
    public class PlayerAimHandler : IFixedTickable
    {
        private const float AimSpeed = 200f;
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
            if (!playerInputState.IsFiring)
            {
                LerpTargetAimTo(Vector3.zero);
                return;
            }

            var rotatedAim = Quaternion.AngleAxis(30f, Vector3.right) * aimLocation;
            var aimTranformVector = rotatedAim - playerModel.Position;
            aimTranformVector.y = aimTranformVector.z;
            aimTranformVector.z = 0f;

            var invertedAimVector = new Vector3(-aimTranformVector.x, aimTranformVector.y);
            var targetVector = aimTranformVector.x >= 0 ? aimTranformVector : invertedAimVector;
            LerpTargetAimTo(targetVector);
             
            playerModel.SpineSkeletonAnimation.Skeleton.ScaleX = aimTranformVector.x >= 0 ? -1 : 1;
        }

        private void LerpTargetAimTo(Vector3 targetVector)
        {
            playerModel.AimTransform.localPosition = Vector3.Lerp(playerModel.AimTransform.localPosition, targetVector, AimSpeed);
        }
    }
}