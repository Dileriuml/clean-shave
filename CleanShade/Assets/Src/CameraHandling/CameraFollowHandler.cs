using Src.Characters.Player;
using UnityEngine;
using Zenject;

namespace Src.CameraHandling
{
    public class CameraFollowHandler : ILateTickable
    {
        private readonly Camera mainCamera;
        private readonly CameraSettings cameraSettings;
        private readonly PlayerModel playerModel;

        public CameraFollowHandler(
            Camera mainCamera, 
            CameraSettings cameraSettings,
            PlayerModel playerModel)
        {
            this.mainCamera = mainCamera;
            this.cameraSettings = cameraSettings;
            this.playerModel = playerModel;
        }
        
        public void LateTick()
        {
            mainCamera.transform.position = Vector3.Lerp(
                mainCamera.transform.position, 
                playerModel.Position + cameraSettings.Offset, 
                cameraSettings.Smoothing * Time.deltaTime);
        }
    }
}