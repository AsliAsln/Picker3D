using Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
   public class CoreUiSignals : MonoBehaviour
   {
      #region Singleton

      public static CoreUiSignals ınstance;

      private void Awake()
      {
         if (ınstance != null && ınstance != this)
         {
            Destroy(gameObject);
            return;
         }

         ınstance = this;
      }

      #endregion

      public UnityAction<UIPanelTypes,int> onOpenPanel = delegate { };
      public UnityAction<int> onClosePanel = delegate { };
      public UnityAction onCloseAllPanels = delegate { };
      


   }
}
