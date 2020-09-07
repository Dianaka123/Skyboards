using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class FloatAnimation
    {
        private float start;
        private float end;
        private float duration;
        private float currentDuration;

        public float Value { get; private set; }

        public void Start(float start, float end, float duration)
        {
            this.start = start;
            this.end = end;
            this.duration = duration;
            currentDuration = 0;
        }

        public bool Update(float delta)
        {
            var animationDelta =  Mathf.Clamp(currentDuration / duration, 0, 1);
            if (animationDelta >= 1)
            {
                return true;
            }
            Value = start + (end - start) * Mathf.Sin(animationDelta * (Mathf.PI / 2));
            currentDuration += delta;
            return false;
        }

    }
}
