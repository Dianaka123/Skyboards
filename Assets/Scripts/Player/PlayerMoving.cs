using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMoving : MonoBehaviour
    {
        private Rigidbody player;
        private Vector3 startPosition;
        private Quaternion startRotation;

        [Range(1, 10)] 
        public int Speed = 8;

        public float XDifference = 515;

        private float tilt = 2f;

        private void Start()
        {
            player = GetComponent<Rigidbody>();
            startPosition = transform.position;
            startRotation = transform.rotation;
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.GameState != GameState.Play)
            {
                return;
            }

            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            player.velocity = new Vector3(horizontal, 0, vertical) * Speed;

            var newPosX = Mathf.Clamp(player.position.x, startPosition.x - XDifference, startPosition.x + XDifference);
            var newPosY = player.position.y;


            player.position = new Vector3(newPosX, newPosY, startPosition.z);

            var newAngle = Quaternion.Euler(player.velocity.z * tilt, 0, -player.velocity.x * tilt);
            newAngle.x = startRotation.x;

            transform.rotation = newAngle;
        }
    }
}
