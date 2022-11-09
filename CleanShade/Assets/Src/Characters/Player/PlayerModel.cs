using UnityEngine;

namespace Src.Characters.Player
{
    public class PlayerModel
    {
        private float health = 100.0f;

        public PlayerModel(MeshRenderer renderer, Rigidbody rigidBody)
        {
            RigidBody = rigidBody;
            Renderer = renderer;
        }

        public MeshRenderer Renderer { get; }

        public Rigidbody RigidBody { get; }

        public bool IsDead { get; set; }

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