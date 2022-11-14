using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Src.Input
{
    public class PlayerInputHandler : ITickable, IFixedTickable
    {
        private readonly IPlayerInputState playerInputState;
        private readonly IMouseRaycastSettings _mouseRaycastSettings;
        
        private readonly PlayerInputActions playerInputActions;
        private readonly Camera mainCamera;
        
        public PlayerInputHandler(
            IPlayerInputState playerInputState,
            IMouseRaycastSettings mouseRaycastSettings,
            Camera mainCamera,
            PlayerInputActions playerInputActions)
        {
            this.mainCamera = mainCamera;
            this._mouseRaycastSettings = mouseRaycastSettings;
            this.playerInputState = playerInputState;
            this.playerInputActions = playerInputActions;
            playerInputActions.Player.Enable();
        }
        
        public Vector2 GetMoveVector() => playerInputActions.Player.Movement.ReadValue<Vector2>();
        
        public void Tick()
        {
            HandlerMovementInput();
            HandleFireInput();
        }

        public void FixedTick() => HandleCursorInput();
        
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
            var positionValue = Mouse.current.position.ReadValue();
            var mouseRayFromCamera = mainCamera.ScreenPointToRay(positionValue);

            if (Physics.Raycast(
                    mouseRayFromCamera, 
                    out var rayHitPosition, 
                    100f, 
                    _mouseRaycastSettings.AimRaycastingMask))
            {
                var point = rayHitPosition.point;
                point.y = 0f;
                playerInputState.AimLocation = point;
            }
        }
    }
}