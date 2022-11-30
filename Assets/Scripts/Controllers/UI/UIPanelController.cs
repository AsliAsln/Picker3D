using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Signals;
using UnityEngine;

namespace Controllers.UI
{
    public class UIPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<Transform> layers = new List<Transform>();
        

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreUiSignals.ınstance.onOpenPanel += OnOpenPanel;
            CoreUiSignals.ınstance.onClosePanel += OnClosePanel;
            CoreUiSignals.ınstance.onCloseAllPanels += OnCloseAllPanel;

        }

        private void OnCloseAllPanel()
        {
            foreach (var t in layers.Where(t=>t.childCount>0))
            {
                Destroy(t.GetChild(0).gameObject);
            }
        }

        private void OnClosePanel(int layerValue)
        {
            if(layers[layerValue].childCount>0)
                Destroy(layers[layerValue].GetChild(0).gameObject);
        }

        private void OnOpenPanel(UIPanelTypes panelType, int layerValue)
        {
            OnClosePanel(layerValue);
            Instantiate(Resources.Load<GameObject>($"Screens/{panelType.ToString()}Panel"),layers[layerValue]);
        }


        private void UnsubscribeEvents()
        {
            throw new NotImplementedException();
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

    }
}
