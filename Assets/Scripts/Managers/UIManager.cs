using System;
using Signals;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    public class UIManager: MonoBehaviour
    {
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void UnsubscribeEvents()
        {
            throw new NotImplementedException();
        }

        private void SubscribeEvents()
        {
            CoreUiSignals.ınstance.OnL
        }

        void OnLevelInitialize()
        {
            
        }
    }
}