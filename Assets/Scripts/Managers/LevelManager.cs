using Command;
using Data.UnityObjects;
using Data.ValueObjects;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Transform levelHolder;
        [SerializeField] private int levelID;

        #endregion

        #region Private Variables

        private LevelData _data;
        private int _totalLevelCount;

        private OnLevelLoaderCommand _levelLoader;
        private OnLevelDestroyerCommand _levelDestroyer;
        private OnRestartLevelCommand _restartLevel;
        private OnNextLevelCommand _nextLevel;

        #endregion

        #endregion

        public static LevelManager Instance;
        
        private void Awake()
        {
            if(Instance!= null && Instance !=this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            
            _data = GetLevelData();
            _totalLevelCount = GetTotalLevelCount();
            levelID = GetLevelID();

            Init();
        }

        private void Init()
        {
            _levelDestroyer = new OnLevelDestroyerCommand(levelHolder);
            _levelLoader = new OnLevelLoaderCommand(levelHolder);
        }

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGamesSignals.Instance.onLevelInitialize += _levelLoader.Execute;
            CoreGamesSignals.Instance.onClearActiveLevel += _levelDestroyer.Execute;
            CoreGamesSignals.Instance.onNextLevel += _nextLevel.Execute;
            CoreGamesSignals.Instance.onRestartLevel += _restartLevel.Execute;
        }

        private void UnsubscribeEvents()
        {
            CoreGamesSignals.Instance.onLevelInitialize -= _levelLoader.Execute;
            CoreGamesSignals.Instance.onClearActiveLevel -= _levelDestroyer.Execute;
            CoreGamesSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGamesSignals.Instance.onRestartLevel -= OnRestartLevel;
        }

        private void OnDisable() => UnsubscribeEvents();

        private void Start()
        {
            _levelLoader.Execute(levelID);
        }

        private LevelData GetLevelData() => Resources.Load<CD_Level>("Data/CD_Level").LevelList[levelID];
        private int GetTotalLevelCount() => Resources.Load<CD_Level>("Data/CD_Level").LevelList.Count;
        private int GetLevelId() => GetLevelId();

        public static int GetLevelID()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("Level") ? ES3.Load<int>("Level") : 0;
        }

        private void OnNextLevel()
        {
            levelID++;
            CoreGamesSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGamesSignals.Instance.onReset?.Invoke();
            CoreGamesSignals.Instance.onLevelInitialize?.Invoke(levelID);
        }

        private void OnRestartLevel()
        {
            CoreGamesSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGamesSignals.Instance.onReset?.Invoke();
            CoreGamesSignals.Instance.onLevelInitialize?.Invoke(levelID);
        }

        public void IncreaseLevelId(int increaseAmount)
        {
            levelID = increaseAmount;
        }
        
    }
}