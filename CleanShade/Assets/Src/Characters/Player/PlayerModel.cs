using System;
using Spine.Unity;
using Src.Characters.Character;
using UnityEngine;

namespace Src.Characters.Player
{
    [Serializable]
    public class PlayerModel : CharacterModel
    {
        public PlayerModel(MeshRenderer renderer, Rigidbody rigidBody, SkeletonAnimation spineSkeletonAnimation, Transform aimTransform) 
            : base(renderer, rigidBody, spineSkeletonAnimation, aimTransform)
        {
        }
    }
}