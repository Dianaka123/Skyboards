using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    public class TimeTextBehaviour: ResetableBehaviour
    {
        private Text text;
        private float time;

        protected override void Start()
        {
            base.Start();
            text = GetComponent<Text>();
        }

        protected override void OnReset()
        {
            base.OnReset();
            time = 0;
        }

        protected void Update()
        {
            if (GameManager.Instance.GameState == GameState.Pause)
            {
                return;
            }
            time += Time.deltaTime;
            text.text = ConvertTime(time);
        }

        private string ConvertTime(float currentTime)
        {
            string FormatUnit(float value) =>
                value < 10 ? $"0{value}" : $"{value}";

            var minutes = (int)Math.Floor(currentTime / 60);
            var seconds = (int)currentTime % 60;
            return $"{FormatUnit(minutes)}:{FormatUnit(seconds)}";
        }

    }
}
