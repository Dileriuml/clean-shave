using UnityEngine;
using Zenject;

namespace Src.Input
{
    public class PlayerInputHandler : ITickable
    {
        private readonly IPlayerInputState playerInputState;
        private readonly PlayerInputActions playerInputActions;
        private readonly Camera mainCamera;
        
        public PlayerInputHandler(
            Camera mainCamera,
            IPlayerInputState playerInputState,
            PlayerInputActions playerInputActions)
        {
            this.mainCamera = mainCamera;
            this.playerInputState = playerInputState;
            this.playerInputActions = playerInputActions;
            playerInputActions.Player.Enable();
        }
        
        public Vector2 GetMoveVector() => playerInputActions.Player.Movement.ReadValue<Vector2>();
        
        public void Tick()
        {
            HandlerMovementInput();
            HandleFireInput();
            HandleCursorInput();
        }

        private void HandleFireInput()
        {
            playerInputState.IsFiring = playerInputActions.Player.Fire.IsPressed();
        }

        private void HandlerMovementInput()
        {
            var moveVector = GetMoveVector();
            playerInputState.MoveVector = new Vector3(moveVector.x, 0, moveVector.y);
        }

        private void HandleCursorInput()
        {
            var mousePos = playerInputActions.Player.MousePosition.ReadValue<Vector2>();
            playerInputState.AimLocation = mainCamera.ScreenPointToRay(mousePos).origin;
        }
    }
}