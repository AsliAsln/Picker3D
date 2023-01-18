using System;
using Data.ValueObjects;
using DG.Tweening;
using Keys;
using Managers;
using Signals;
using Unity.Mathematics;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;

        [SerializeField] public new Rigidbody rigidbody;
        [SerializeField] private new Collider collider;

        #endregion

        #region Private Variables

        [SerializeField] public MovementData _data;

        [SerializeField] private bool _isReadyToMove, _isReadyToPlay;

        private float _xValue;
        private float2 _clampValues;

        #endregion

        #endregion
        
        internal void SetMovementData(MovementData movementData)
        {
            _data = movementData;
        }

        private void FixedUpdate()
        {
            if (!_isReadyToPlay)
            {
                StopPlayer();
                return;
            }

            if (_isReadyToMove)
            {
                MovePlayer();
            }
            else
            {
                StopPlayerHorizontaly();
            }
        }

        private void MovePlayer()
        {
            var velocity = rigidbody.velocity;
            velocity = new float3(_xValue * _data.SidewaysSpeed, velocity.y,
                _data.ForwardSpeed);
            rigidbody.velocity = velocity;

            float3 position;
            position = new float3(
                Mathf.Clamp(rigidbody.position.x, _clampValues.x,
                    _clampValues.y),
                (position = rigidbody.position).y,
                position.z);
            rigidbody.position = position;
        }

        public void MiniGameMove()
        {
            var tempSpeed = _data.ForwardSpeed * _data.MiniGameMultiplier;
            DOTween.To(() => tempSpeed, x => tempSpeed = x, 5, UnityEngine.Random.Range(4,10))
                .OnUpdate(() =>
                {
                    _data.ForwardSpeed = tempSpeed;
                })
                .OnComplete(() => 
                {
                _data.ForwardSpeed = 0;
                DOVirtual.DelayedCall(3, () =>
                {
                    CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
                });
            });
            }
          
        private void StopPlayerHorizontaly()
        {
            rigidbody.velocity = new float3(0, rigidbody.velocity.y, _data.ForwardSpeed);
            rigidbody.angularVelocity = float3.zero;
        }

        private void StopPlayer()
        {
            rigidbody.velocity = float3.zero;
            rigidbody.angularVelocity = float3.zero;
        }

        internal void IsReadyToPlay(bool condition)
        {
            _isReadyToPlay = condition;
        }

        internal void IsReadyToMove(bool condition)
        {
            _isReadyToMove = condition;
        }

        internal void UpdateInputParams(HorizontalnputParams inputParams)
        {
            _xValue = inputParams.HorizontalInputValue;
            _clampValues = new float2(inputParams.HorizontalInputClampNegativeSide,
                inputParams.HorizontalInputClampPositiveSide);
        }

        internal void OnReset()
        {
            StopPlayer();
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }

        public float GetBonusMult()
        {
            return _data.MiniGameMultiplier;
        }
    }
}