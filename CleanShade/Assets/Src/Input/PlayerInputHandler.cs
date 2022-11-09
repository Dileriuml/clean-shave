using UnityEngine;
using Zenject;

namespace Src.Input
{
    public class PlayerInputHandler : IPlayerInputHandler, ITickable
    {
        private readonly IPlayerInputState playerInputState;
        private readonly PlayerInputActions playerInputActions;
        
        public PlayerInputHandler(IPlayerInputState playerInputState, PlayerInputActions playerInputActions)
        {
            this.playerInputState = playerInputState;
            this.playerInputActions = playerInputActions;
            playerInputActions.Player.Enable();
        }
        
        public Vector2 GetMoveVector() => playerInputActions.Player.Movement.ReadValue<Vector2>();
        
        public void Tick()
        {
            var moveVector = GetMoveVector();
            playerInputState.MoveVector = new Vector3(moveVector.x, 0, moveVector.y);
            playerInputState.IsFiring = playerInputActions.Player.Fire.IsPressed();
        }
    }
}