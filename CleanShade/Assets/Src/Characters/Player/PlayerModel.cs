using System;
using Spine.Unity;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Src.Characters.Player
{
    [Serializable]
    public class PlayerModel
    {
        private float health = 100.0f;

        [SerializeField]
        private Vector3 aimVector;
        
        public PlayerModel(
            MeshRenderer renderer, 
            Rigidbody rigidBody, 
            SkeletonAnimation spineSkeletonAnimation)
        {
            RigidBody = rigidBody;
            Renderer = renderer;
            SpineSkeletonAnimation = spineSkeletonAnimation;
        }

        public MeshRenderer Renderer { get; }

        public Rigidbody RigidBody { get; }

        public SkeletonAnimation SpineSkeletonAnimation { get; }

        public float Health => health;

        public Vector3 AimVector
        {
            get => aimVector;
            set => aimVector = value;
        }

        public Quaternion Rotation
        {
            get => RigidBody.rotation;
            set => RigidBody.rotation = value;
        }

        public Vector3 Position
        {
            get => RigidBody.position;
            set => RigidBody.position = value;
        }

        public void TakeDamage(float healthLoss)
        {
            health = Mathf.Max(0.0f, health - healthLoss);
        }
    }
}