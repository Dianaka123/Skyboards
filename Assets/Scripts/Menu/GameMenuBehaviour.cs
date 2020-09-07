using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;

public class GameMenuBehaviour : MonoBehaviour
{

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (GameManager.Instance.GameState == GameState.GameOver)
            {
                return;
            }
            GameManager.Instance.GameState = 
                GameManager.Instance.GameState == GameState.Play ? GameState.Pause : GameState.Play;
        }
    }
}
