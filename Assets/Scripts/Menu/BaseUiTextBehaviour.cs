using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    public class BaseUITextBehaviour : ResetableBehaviour
    {
        private readonly Func<GameManager, object> getter;
        private readonly string propertyName;

        protected Text Text { get; private set; }

        public BaseUITextBehaviour(Func<GameManager, object> getter, string propertyName)
        {
            this.getter = getter;
            this.propertyName = propertyName;
        }

        protected override void Start()
        {
            base.Start();
            Text = GetComponent<Text>();
            ResetText();
            GameManager.Instance.PropertyChanged += InstanceOnPropertyChanged;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GameManager.Instance.PropertyChanged -= InstanceOnPropertyChanged;
        }

        protected virtual void OnChanged()
        {

        }

        private void InstanceOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == propertyName)
            {
                ResetText();
                OnChanged();
            }
        }

        private void ResetText()
        {
            Text.text = getter(GameManager.Instance).ToString();
        }
    }

}
