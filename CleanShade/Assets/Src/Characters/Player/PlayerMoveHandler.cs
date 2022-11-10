using System;
using Src.Input;
using UnityEngine;
using Zenject;

namespace Src.Characters.Player
{
    public class PlayerMoveHandler : IFixedTickable
    {
        private static readonly Quaternion RotatedVector = Quaternion.AngleAxis( 45f, Vector3.up);
        private readonly CharactersSettings.Player settings;
        private readonly PlayerModel player;
        private readonly PlayerState playerState;
        private readonly IPlayerInputState inputState;

        public PlayerMoveHandler(
            IPlayerInputState inputState,
            PlayerModel player,
            PlayerState playerState,
            CharactersSettings.Player settings)
        {
            this.settings = settings;
            this.player = player;
            this.playerState = playerState;
            this.inputState = inputState;
        }

        public void FixedTick()
        {
            if (playerState.IsDead)
            {
                return;
            }

            var calculatedMoveVector = (RotatedVector * inputState.MoveVector) * (settings.Speed * Time.deltaTime);
            player.RigidBody.position += calculatedMoveVector;

            // Always ensure we are on the main plane
            //player.Position = new Vector3(player.Position.x, player.Position.y, 0);
        }
    }
}