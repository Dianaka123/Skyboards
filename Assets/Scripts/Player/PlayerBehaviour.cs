using System.ComponentModel;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerBehaviour : ResetableBehaviour
    {
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private AudioClip collisionClip;
        
        protected override void OnReset()
        {
            base.OnReset();

            // Reset geometry properties
            var player = GetComponent<Rigidbody>();
            player.velocity = new Vector3(0, 0, 0);

            gameObject.transform.position = new Vector3(0f,
                gameObject.transform.position.y, gameObject.transform.position.z);

            gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Asteroid"))
            {
                soundManager.CollisionAsteroid.PlayOneShot(collisionClip);
                GameManager.Instance.GameState = GameState.GameOver;
                gameObject.SetActive(false);
            }
        }
    }
}
