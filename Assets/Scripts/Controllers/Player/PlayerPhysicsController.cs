using System;
using Controllers.Pool;
using Controllers.UI;
using DG.Tweening;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private new Collider collider;
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private PlayerMovementController _playerMovementController;
        [SerializeField] private LevelPanelController _levelPanelController;

        #endregion

        #endregion

      
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("StageArea"))
            {
                manager.ForceCommand.Execute();
                CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();

                DOVirtual.DelayedCall(3, () =>
                {
                    var result = other.transform.parent.GetComponentInChildren<PoolController>()
                        .TakeStageResult(manager.StageID);
                    if (result)
                    {
                        CoreGameSignals.Instance.onStageAreaSuccessful?.Invoke(manager.StageID);
                        UISignals.Instance.onSetStageColor?.Invoke(manager.StageID);
                        InputSignals.Instance.onEnableInput?.Invoke();
                        manager.StageID++;
                    }
                    else CoreGameSignals.Instance.onLevelFailed?.Invoke();
                });
                return;
            }
            else if (other.CompareTag("BonusArea"))
            {
                CoreGameSignals.Instance.onBonusAreaEntered.Invoke();
                rigidbody.velocity *= _playerMovementController.GetBonusMult();
                GameObject particle = manager.transform.Find("particle").gameObject;
                particle.SetActive(true);
                
            }
            else if (other.CompareTag("bonus"))
            {

                if (_playerMovementController.rigidbody.velocity.z<=0)
                {
                    Debug.Log("hız 0");
                    _levelPanelController._diamondText.text = other.GetComponent<BonusItem>()._itemNumber.ToString();
                    other.GetComponent<Renderer>().material.color=Color.red;
                    Debug.Log(other.GetComponent<Renderer>().material.color);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            var transform1 = manager.transform;
            var position = transform1.position;
            Gizmos.DrawSphere(new Vector3(position.x, position.y - 1.2f, position.z + 1f), 1.65f);
        }

        public void OnReset()
        {
           
        }
    }
}