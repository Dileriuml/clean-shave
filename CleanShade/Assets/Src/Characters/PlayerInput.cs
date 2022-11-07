using UnityEngine;

namespace Src.Characters
{
    public class PlayerInput : IPlayerInput
    {
        private readonly PlayerInputActions playerInputActions = new();
        
        public PlayerInput()
        {
            playerInputActions.Player.Enable();
        }
        
        public Vector2 GetMoveVector() => playerInputActions.Player.Movement.ReadValue<Vector2>();
    }
}