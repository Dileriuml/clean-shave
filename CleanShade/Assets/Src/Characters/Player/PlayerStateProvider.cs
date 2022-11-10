using UnityEngine;
using Zenject;

namespace Src.Characters.Player
{
    public class PlayerStateProvider : ITickable
    {
        private const float EpsilonMoveChange = 10e-3f;
        private const float EpsilonLowestHealth = 10e-4f;
        private const float EpsilonStopThresholdSeconds = 0.2f;
        
        private float lastTimeMoving;
        private Vector3 positionAtLastFrame;
        
        private readonly PlayerState playerState;
        private readonly PlayerModel playerModel;
        
        public PlayerStateProvider(
            PlayerModel playerModel,
            PlayerState playerState)
        {
            this.playerModel = playerModel;
            this.playerState = playerState;
        }
        
        public void Tick()
        {
            ProvideMoveState();
            ProvideAliveState();
        }

        private void ProvideAliveState()
        {
            playerState.IsDead = playerModel.Health <= EpsilonLowestHealth;
        }

        private void ProvideMoveState()
        {
            var positionChanged = positionAtLastFrame - playerModel.Position;
            if (positionChanged.magnitude <= EpsilonMoveChange)
            {
                if (Time.realtimeSinceStartup - lastTimeMoving > EpsilonStopThresholdSeconds)
                {
                    playerState.IsMoving = false;
                }
            }
            else
            {
                lastTimeMoving = Time.realtimeSinceStartup;
                playerState.IsMoving = true;
            }
            
            positionAtLastFrame = playerModel.Position;
        }
    }
}