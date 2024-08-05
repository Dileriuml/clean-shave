using Src.Characters.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Src.Input
{
    public class PlayerInputHandler : ITickable, IFixedTickable
    {
        private readonly IPlayerInputState playerInputState;
        private readonly IMouseRaycastSettings mouseRaycastSettings;
        
        private readonly PlayerInputActions playerInputActions;
        private readonly Camera mainCamera;
        private readonly Transform playerSpriteTransform;

        public PlayerInputHandler(
            IPlayerInputState playerInputState,
            IMouseRaycastSettings mouseRaycastSettings,
            Camera mainCamera,
            PlayerModel playerModel,
            PlayerInputActions playerInputActions)
        {
            this.mainCamera = mainCamera;
            this.mouseRaycastSettings = mouseRaycastSettings;
            this.playerInputState = playerInputState;
            this.playerInputActions = playerInputActions;
            this.playerSpriteTransform = playerModel.Renderer.transform;
            playerInputActions.Player.Enable();
        }
        
        public void Tick()
        {
            HandleMovementInput();
            HandleFireInput();
        }

        public void FixedTick() => HandleCursorInput();
        
        private void HandleFireInput()
        {
            playerInputState.IsFiring = playerInputActions.Player.Fire.IsPressed();
        }

        private Vector2 GetMoveVector() => playerInputActions.Player.Movement.ReadValue<Vector2>();

        private void HandleMovementInput()
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
                    mouseRaycastSettings.AimRaycastingMask))
            {
                var point = rayHitPosition.point;
                point.y = 0f;
                playerInputState.AimLocation = point;
            }
        }
    }
}