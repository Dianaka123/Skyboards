using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    class GameOverMenuBehaviour : MonoBehaviour
    {
        private static readonly HashSet<KeyCode> restrictedKeys = new HashSet<KeyCode>
        {
            KeyCode.Escape,
            KeyCode.P,
            KeyCode.Space
        };

        private void Update()
        {
            if (Input.anyKeyDown && !restrictedKeys.Any(Input.GetKeyDown))
            {
                GameManager.Instance.GameState = GameState.StartMenu;
            }
        }
    }
}
