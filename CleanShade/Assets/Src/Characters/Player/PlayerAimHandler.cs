using Src.Input;
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
        }
    }
}