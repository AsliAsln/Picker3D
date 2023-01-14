using Data.ValueObjects;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        [SerializeField]private ScaleData _data;

        #endregion

        #endregion

        public void SetMeshData(ScaleData scaleData)
        {
            _data = scaleData;
        }

        public void OnReset()
        {
        }
    }
}