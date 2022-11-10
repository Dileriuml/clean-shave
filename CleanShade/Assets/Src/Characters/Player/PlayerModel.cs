using Spine.Unity;
using UnityEngine;

namespace Src.Characters.Player
{
    public class PlayerModel
    {
        private float health = 100.0f;

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