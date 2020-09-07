using Assets.Scripts.Managers;

namespace Assets.Scripts.Menu
{
    
    public class ScoreTextBehaviour : BaseUITextBehaviour
    {
        public ScoreTextBehaviour() : 
            base(x => x.Score, nameof(GameManager.Score))
        { }
    }
}