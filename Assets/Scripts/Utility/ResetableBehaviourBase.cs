using System.ComponentModel;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class ResetableBehaviour: MonoBehaviour
    {
        protected virtual void Start()
        {
            GameManager.Instance.PropertyChanged += InstanceOnPropertyChanged;
        }

        protected virtual void OnDestroy()
        {
            GameManager.Instance.PropertyChanged -= InstanceOnPropertyChanged;
        }

        private void InstanceOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GameManager.GameState) && GameManager.Instance.GameState == GameState.StartMenu)
            {
                OnReset();
            }
        }

        protected virtual void OnReset()
        {
        }
    }
}
