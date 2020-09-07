using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Assets.Scripts.Managers;
using UnityEngine;

public class UIManagerBehaviour : MonoBehaviour
{
    private GameObject[] currentMenus = new GameObject[] { };

    public GameObject StartMenu;

    public GameObject PlayMenu;

    public GameObject PauseMenu;

    public GameObject GameOverMenu;

    private void Start()
    {
        ResetMenus();
        GameManager.Instance.PropertyChanged += InstanceOnPropertyChanged;
    }

    private void OnDestroy()
    {
        GameManager.Instance.PropertyChanged -= InstanceOnPropertyChanged;
    }

    private void InstanceOnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(GameManager.GameState):
                ResetMenus();
                break;
        }
    }

    private void ResetMenus()
    {
        SetActiveMenus(false);
        var state = GameManager.Instance.GameState;
        switch (state)
        {
            case GameState.Play:
                currentMenus = new[] { PlayMenu };
                break;
            case GameState.Pause:
                currentMenus = new[] { PlayMenu, PauseMenu };
                break;
            case GameState.GameOver:
                currentMenus = new[] { PlayMenu, GameOverMenu };
                break;
            case GameState.StartMenu:
                currentMenus = new[] { StartMenu };
                break;
        }
        SetActiveMenus(true);
    }

    private void SetActiveMenus(bool active)
    {
        foreach (var menu in currentMenus)
        {
            menu.SetActive(active);
        }
    }
}
