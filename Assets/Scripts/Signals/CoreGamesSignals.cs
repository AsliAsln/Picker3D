using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoreGamesSignals : MonoBehaviour
{
    #region Singleton

    public static CoreGamesSignals Instance;

    private void Awake()
    {
        if(Instance!= null && Instance !=this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion
    public UnityAction<GameStates> onChangeGameState = delegate { };
    public UnityAction<int> onLevelInitialize = delegate { };
    public UnityAction onClearActiveLevel = delegate { };
    public UnityAction onLevelFailed = delegate { };
    public UnityAction onLevelSuccessful = delegate { };
    public UnityAction onNextLevel = delegate { };
    public UnityAction onRestartLevel = delegate { };
    public UnityAction onPlay = delegate { };
    public UnityAction onReset = delegate { };







}
