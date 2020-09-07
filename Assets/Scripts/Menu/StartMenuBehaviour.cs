using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class StartMenuBehaviour : MonoBehaviour
    {
        private static readonly HashSet<KeyCode> restrictedKeys = new HashSet<KeyCode>
        {
            KeyCode.Escape,
            KeyCode.P,
            KeyCode.Space
        };

        void Update()
        {
            if (Input.anyKeyDown && !restrictedKeys.Any(Input.GetKeyDown))
            {
                GameManager.Instance.GameState = GameState.Play;
            }
        }
    }
}
