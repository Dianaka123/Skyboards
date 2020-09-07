using Assets.Scripts.Managers;

namespace Assets.Scripts.Menu
{
    public class PassedAsteroidTextBehaviour : BaseUITextBehaviour
    {
        public PassedAsteroidTextBehaviour() 
            : base(x => x.PassedAsteroids, nameof(GameManager.PassedAsteroids))
        {
        }
    }
}
