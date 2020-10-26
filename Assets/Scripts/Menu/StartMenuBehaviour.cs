using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class StartMenuBehaviour : MonoBehaviour
    {
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private AudioClip audioClip;
       
        

        private static readonly HashSet<KeyCode> restrictedKeys = new HashSet<KeyCode>
        {
            KeyCode.Escape,
            KeyCode.P,
            KeyCode.Space
        };

        void Update()
        {
            if (Input.GetKey(KeyCode.Return) && !restrictedKeys.Any(Input.GetKeyDown))
            {
                soundManager.UiSource.PlayOneShot(audioClip, 1);
                GameManager.Instance.GameState = GameState.Play;
            }
        }
    }
}
