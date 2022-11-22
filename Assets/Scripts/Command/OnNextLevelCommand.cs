using Managers;

namespace Command
{
    public class OnNextLevelCommand
    {
    
        public void Execute()
        {
            LevelManager.Instance.IncreaseLevelId(1);
            CoreGamesSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGamesSignals.Instance.onReset?.Invoke();
            CoreGamesSignals.Instance.onLevelInitialize?.Invoke(LevelManager.GetLevelID());

        }
    }
}