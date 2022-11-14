using System;
using Src.Audio;
using Src.Characters.Shooting;
using Src.Input;
using UnityEngine;
using Zenject;

namespace Src.Characters.Player
{
    public class PlayerShootHandler : ITickable
    {
        private readonly IAudioPlayer audioPlayer;
        private readonly PlayerState playerState;
        private readonly Settings settings;
        private readonly PlayerModel playerModel;
        private readonly Bullet.Factory bulletFactory;
        private readonly IPlayerInputState inputState;

        private float lastFireTime;

        public PlayerShootHandler(
            IPlayerInputState inputState,
            Bullet.Factory bulletFactory,
            Settings settings,
            PlayerModel playerModel,
            PlayerState playerState,
            IAudioPlayer audioPlayer)
        {
            this.audioPlayer = audioPlayer;
            this.playerState = playerState;
            this.playerModel = playerModel;
            this.settings = settings;
            this.bulletFactory = bulletFactory;
            this.inputState = inputState;
        }

        public void Tick()
        {
            if (playerState.IsDead)
            {
                return;
            }
            
            playerState.IsFiring = inputState.IsFiring;

            if (inputState.IsFiring && 
                Time.realtimeSinceStartup - lastFireTime > settings.MaxShootInterval)
            {
                lastFireTime = Time.realtimeSinceStartup;
                Fire();
            }
        }

        private void Fire()
        {
            if (settings.ShootingSound != null)
            {
                audioPlayer.Play(settings.ShootingSound, settings.LaserVolume);
            }

            var bullet = bulletFactory.Create(
                settings.BulletSpeed,
                settings.BulletLifetime, 
                BulletOwnerType.FromPlayer);
            
            bullet.transform.position = playerModel.Position + playerModel.AimVector * settings.BulletOffsetDistance;
            bullet.transform.LookAt(playerModel.AimVector);
        }

        [Serializable]
        public class Settings
        {
            public AudioClip ShootingSound;
            public float LaserVolume = 1.0f;

            public float BulletLifetime;
            public float BulletSpeed;
            public float MaxShootInterval;
            public float BulletOffsetDistance;
        }
    }
}