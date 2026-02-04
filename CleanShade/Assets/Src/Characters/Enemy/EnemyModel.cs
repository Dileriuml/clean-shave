using Spine.Unity;
using Src.Characters.Character;
using UnityEngine;

namespace Src.Characters.Enemy
{
    public class EnemyModel : CharacterModel
    {
        public EnemyModel(MeshRenderer renderer, Rigidbody rigidBody, SkeletonAnimation spineSkeletonAnimation, Transform aimTransform) 
            : base(renderer, rigidBody, spineSkeletonAnimation, aimTransform)
        {
        }
    }
}