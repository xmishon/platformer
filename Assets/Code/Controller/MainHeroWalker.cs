using UnityEngine;

namespace PlatformerMVC
{
    public class MainHeroWalker
    {
        private const float _walkSpeed = 3f;
        private const float _animationSpeed = 10f;
        private const float _jumpStartSpeed = 8f;
        private const float _movingThresh = 0.1f;
        private const float _flyThresh = 1f;
        private const float _groundLevel = 0.5f;
        private const float _g = -9.81f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private float _yVelocity;
        private float _xAxisInput = 0;
        private bool _doJump = false;

        private LevelObjectView _view;
        private SpriteAnimator _spriteAnimator;

        public MainHeroWalker(LevelObjectView view, SpriteAnimator spriteAnimator)
        {
            _view = view;
            _spriteAnimator = spriteAnimator;
        }

        public void Update()
        {
            _doJump = Input.GetAxis("Vertical") > 0;
            _xAxisInput = Input.GetAxis("Horizontal");

            var goSideWay = Mathf.Abs(_xAxisInput) > _movingThresh;

            if(IsGrounded())
            {
                if (goSideWay) GoSideWay();
                _spriteAnimator.StartAnimation(_view.spriteRenderer, goSideWay ? AnimationState.Run : AnimationState.Idle, true, _animationSpeed);

                if(_doJump && _yVelocity == 0)
                {
                    _yVelocity = _jumpStartSpeed;
                }
                else if(_yVelocity < 0)
                {
                    _yVelocity = 0;
                    _view.transform.position = _view.transform.position.Change(y: _groundLevel);
                }
            }
            else
            {
                if (goSideWay) GoSideWay();
                if(Mathf.Abs(_yVelocity) > _flyThresh)
                {
                    _spriteAnimator.StartAnimation(_view.spriteRenderer, AnimationState.Jump, true, _animationSpeed);
                }
                _yVelocity += _g * Time.deltaTime;
                _view.transform.position += Vector3.up * (Time.deltaTime * _yVelocity);
            }
        }

        private void GoSideWay()
        {
            _view.transform.position += Vector3.right * (Time.deltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1));
            _view.transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
        }

        public bool IsGrounded()
        {
            return _view.transform.position.y <= _groundLevel + float.Epsilon && _yVelocity <= 0;
        }
    }
}
