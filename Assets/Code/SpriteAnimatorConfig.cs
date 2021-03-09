using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    [CreateAssetMenu(fileName = "SpriteAnimationConfig", menuName = "Configs/AnimationConfig", order = 1)]
    public class SpriteAnimatorConfig : ScriptableObject
    {
        [Serializable]
        public sealed class SpriteSequence
        {
            public AnimationState State;
            public List<Sprite> Sprites = new List<Sprite>();
        }

        public List<SpriteSequence> Sequences = new List<SpriteSequence>();
    }
}

