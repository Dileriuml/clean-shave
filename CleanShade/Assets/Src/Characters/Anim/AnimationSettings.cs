using System;
using UnityEngine;

namespace Src.Characters.Anim
{
    [Serializable]
    public class AnimationSettings
    {
        [SerializeField]
        private float moveAnimationSpeed = 1f;
        
        [SerializeField]
        private float fireAnimationSpeed = 1f;

        public float MoveAnimationSpeed => moveAnimationSpeed;

        public float FireAnimationSpeed => fireAnimationSpeed;
    }
}