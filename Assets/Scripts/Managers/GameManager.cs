using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Self Variables


    #region Serialized Variables
    [SerializeField] private GameStates states;


    #endregion

    #endregion

    private void OnEnable() => SubscribeEvents();

    private void SubscribeEvents()
    {
        CoreGamesSignals.Instance.onChangeGameState += OnChangeGameState;
    }
    private void UnSubscribeEvents()
    {
        CoreGamesSignals.Instance.onChangeGameState -= OnChangeGameState;
    }
    private void OnDisable() => UnSubscribeEvents();

    private void OnChangeGameState(GameStates state)
    {
        states = state;
    }

}
