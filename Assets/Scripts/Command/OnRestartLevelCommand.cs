using Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace Command
{
    public class OnRestartLevelCommand
    {
     
        
        public void Execute()
        {
            CoreGamesSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGamesSignals.Instance.onReset?.Invoke();
            CoreGamesSignals.Instance.onLevelInitialize?.Invoke(LevelManager.GetLevelID());
        }
    }
}