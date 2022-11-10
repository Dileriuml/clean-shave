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
            Debug.Log($"{playerInputState.AimLocation.x} {playerInputState.AimLocation.y} {playerInputState.AimLocation.z}");
            playerModel.AimVector = (playerModel.Position - playerInputState.AimLocation).normalized;
        }
    }
}