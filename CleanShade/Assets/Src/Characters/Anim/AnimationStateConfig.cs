using System;
using Spine.Unity;

namespace Src.Characters.Anim
{
    [Serializable]
    public class AnimationStateConfig
    {
        public AnimationReferenceAsset Asset;

        public float DefaultTimeScale = 1f;
    }
}