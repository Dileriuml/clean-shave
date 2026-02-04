using System;
using Spine.Unity;
using UnityEngine;

namespace Src.Characters.Character
{
    [Serializable]
    public class CharacterModel
    {
        private float health = 100.0f;

        [SerializeField]
        private Vector3 aimVector;
        
        public CharacterModel(
            MeshRenderer renderer, 
            Rigidbody rigidBody, 
            SkeletonAnimation spineSkeletonAnimation,
            Transform aimTransform)
        {
            RigidBody = rigidBody;
            Renderer = renderer;
            SpineSkeletonAnimation = spineSkeletonAnimation;
            AimTransform = aimTransform;
        }

        public MeshRenderer Renderer { get; }

        public Rigidbody RigidBody { get; }

        public SkeletonAnimation SpineSkeletonAnimation { get; }

        public Transform AimTransform { get; }

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