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
        private readonly Camera camera;

        private float lastFireTime;

        public PlayerShootHandler(
            IPlayerInputState inputState,
            Bullet.Factory bulletFactory,
            Settings settings,
            PlayerModel playerModel,
            PlayerState playerState,
            IAudioPlayer audioPlayer,
            Camera camera)
        {
            this.audioPlayer = audioPlayer;
            this.playerState = playerState;
            this.playerModel = playerModel;
            this.settings = settings;
            this.bulletFactory = bulletFactory;
            this.inputState = inputState;
            this.camera = camera;
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

            var aimVectorLocalized = playerModel.AimVector - playerModel.Position;

            var elevatedAim = playerModel.AimVector;
            elevatedAim.y = settings.ShotHeight;
            var elevatedPosition = playerModel.Position + aimVectorLocalized.normalized * settings.BulletOffsetDistance;
            elevatedPosition.y = settings.ShotHeight;
            
            bullet.transform.position = elevatedPosition;
            bullet.transform.LookAt(elevatedAim);
            bullet.ChildTransform.LookAt(camera.transform);
            bullet.ChildTransform.Rotate(Vector3.forward, bullet.transform.eulerAngles.y + settings.AdditionalIzometricRotation);
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
            public float ShotHeight;
            public float AdditionalIzometricRotation = 90f;
        }
    }
}