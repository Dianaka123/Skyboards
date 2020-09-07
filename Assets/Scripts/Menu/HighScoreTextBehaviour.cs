using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class HighScoreTextBehaviour : BaseUITextBehaviour
    {
        private Color initialColor;
        
        public HighScoreTextBehaviour()
            : base(x => x.HighScore, nameof(GameManager.HighScore))
        { }

        protected override void Start()
        {
            base.Start();
            initialColor = Text.color;
        }

        protected override void OnReset()
        {
            base.OnReset();
            Text.color = initialColor;
        }

        protected override void OnChanged()
        {
            base.OnChanged();
            Text.color = Color.red;
        }
    }
}
