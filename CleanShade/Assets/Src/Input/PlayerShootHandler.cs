using System;
using Src.Audio;
using Src.Characters.Player;
using Src.Input;
using UnityEngine;
using Zenject;

namespace Src.Characters
{
    public class PlayerShootHandler : ITickable
    {
        private readonly AudioPlayer audioPlayer;
        private readonly PlayerModel playerModel;
        private readonly PlayerState playerState;
        private readonly Settings settings;
        //readonly Bullet.Factory _bulletFactory;
        private readonly IPlayerInputState inputState;

        float lastFireTime;

        public PlayerShootHandler(
            IPlayerInputState inputState,
          //  Bullet.Factory bulletFactory,
            Settings settings,
            PlayerModel playerModel,
            PlayerState playerState,
            AudioPlayer audioPlayer)
        {
            this.audioPlayer = audioPlayer;
            this.playerState = playerState;
            this.playerModel = playerModel;
            this.settings = settings;
            //_bulletFactory = bulletFactory;
            this.inputState = inputState;
        }

        public void Tick()
        {
            if (playerState.IsDead)
            {
                return;
            }

            if (inputState.IsFiring && Time.realtimeSinceStartup - lastFireTime > settings.MaxShootInterval)
            {
                lastFireTime = Time.realtimeSinceStartup;
                Fire();
            }
        }

        void Fire()
        {
            audioPlayer.Play(settings.Laser, settings.LaserVolume);

            // var bullet = _bulletFactory.Create(
            //     _settings.BulletSpeed, _settings.BulletLifetime, BulletTypes.FromPlayer);
            //
            // bullet.transform.position = _player.Position + _player.LookDir * _settings.BulletOffsetDistance;
            // bullet.transform.rotation = _player.Rotation;
        }

        [Serializable]
        public class Settings
        {
            public AudioClip Laser;
            public float LaserVolume = 1.0f;

            public float BulletLifetime;
            public float BulletSpeed;
            public float MaxShootInterval;
            public float BulletOffsetDistance;
        }
    }
}