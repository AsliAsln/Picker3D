using System;
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
        private int _totalLevelCount;

        #endregion

        #region Private Variables

        private LevelData _data;

        private OnLevelLoaderCommand _levelLoader;
        private OnLevelDestroyerCommand _levelDestroyer;

        #endregion

        #endregion

        private void Awake()
        {
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
            CoreGamesSignals.Instance.onLevelInitialize
        }

        private void UnsubscribeEvents()
        {
        }

        private void OnDisable() => UnsubscribeEvents();

        private void Start()
        {
            OnInitializeLevel(levelID);
        }

        private LevelData GetLevelData() => Resources.Load<CD_Level>("Data/CD_Level").LevelList[levelID];
        private int GetTotalLevelCount() => Resources.Load<CD_Level>("Data/CD_Level").LevelList.Count;


        private int GetLevelID()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("Level") ? ES3.Load<int>("Level") : 0;
        }

        private void OnInitializeLevel(int ID)
        {
            _levelLoader.Execute(ID);
        }

        private void OnClearActiveLevel()
        {
            _levelDestroyer.Execute();
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
        
    }
}