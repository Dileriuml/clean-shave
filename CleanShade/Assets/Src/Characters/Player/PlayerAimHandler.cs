using System;
using Src.Input;
using Zenject;
using Vector3 = UnityEngine.Vector3;

namespace Src.Characters.Player
{
    public class PlayerAimHandler : IFixedTickable
    {
        private readonly Settings aimSettings;
        private readonly PlayerModel playerModel;
        private readonly IPlayerInputState playerInputState;
        
        public PlayerAimHandler(
            IPlayerInputState playerInputState,
            PlayerModel playerModel,
            Settings aimSettings)
        {
            this.aimSettings = aimSettings;
            this.playerInputState = playerInputState;
            this.playerModel = playerModel;
        }
        
        public void FixedTick()
        {
            playerModel.AimVector = playerInputState.AimLocation;

            HandleAimBowTransform(playerInputState.AimLocation);
        }

        private void HandleAimBowTransform(Vector3 aimLocation)
        {
            if (!playerInputState.IsFiring)
            {
                LerpTargetAimTo(Vector3.zero);
                return;
            }
    
            var aimTranformVector = aimLocation - playerModel.Position;
            aimTranformVector.y = aimTranformVector.z;
            aimTranformVector.z = 0f;
            
            var invertedAimVector = new Vector3(-aimTranformVector.x, aimTranformVector.y);
            var targetVector = aimTranformVector.x >= 0 ? aimTranformVector : invertedAimVector;
            LerpTargetAimTo(targetVector);
            
            playerModel.SpineSkeletonAnimation.Skeleton.ScaleX = aimTranformVector.x >= 0 ? -1 : 1;
        }

        private void LerpTargetAimTo(Vector3 targetVector)
        {
            playerModel.AimTransform.localPosition = Vector3.Lerp(
                playerModel.AimTransform.localPosition, 
                targetVector, 
                aimSettings.AimSpeedScale);
        }
        
        [Serializable]
        public class Settings
        {
            public float AimSpeedScale = 0.1f;
        }
    }
}