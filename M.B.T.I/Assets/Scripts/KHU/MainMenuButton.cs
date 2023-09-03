using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenuButton : MonoBehaviour
{
    public UnityEvent<bool> gameStartEvent;

    private void Start()
    {
        if (gameStartEvent == null)
        {
            gameStartEvent = new UnityEvent<bool>();
        }
    }

    public void OnClickedTutorialBtn()
    {
        OnTutorialEvent();
    }

    public void OnClickedGameStartBtn()
    {
        OnGameStartEvent();
    }

    private void OnGameStartEvent()
    {
        gameStartEvent.Invoke(false);
    }

    private void OnTutorialEvent()
    {
        gameStartEvent.Invoke(false);
    }
}
