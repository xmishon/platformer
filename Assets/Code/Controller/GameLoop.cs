using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private float _animationSpeed = 10f;

        private SpriteAnimator _playerAnimator;

        public void Awake()
        {
            SpriteAnimatorConfig playerConfig = Resources.Load<SpriteAnimatorConfig>("AnimationPlayerConfig");
            _playerAnimator = new SpriteAnimator(playerConfig);
            _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimationState.Idle, true, _animationSpeed);
        }

        public void Update()
        {
            _playerAnimator.Update();
        }

        public void FixedUpdate()
        {

        }
    }
}
