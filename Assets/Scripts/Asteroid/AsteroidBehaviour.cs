using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Asteroid
{
    public class AsteroidBehaviour : MonoBehaviour
    {
        private GameObject plane;

        public int Speed;

        void Start()
        {
            plane = GameObject.Find("Floor");
        }

        void FixedUpdate()
        {
            float currentSpeed = Speed;
            if (Input.GetKey(KeyCode.Space))
            {
                currentSpeed *= GameManager.Instance.SpeedModifier;
            }
            var time = Time.deltaTime;
            transform.Rotate(new Vector3(0, 90, 0) * time, Space.Self);

            if (GameManager.Instance.GameState != GameState.Play)
            {
                return;
            }
            transform.Translate(-Vector3.Normalize(plane.transform.forward) * currentSpeed * time, Space.World);

            var camera = Camera.main;
            var diffAsteroidVector = Vector3.Normalize(transform.position - camera.transform.position);
            var angleCos = Vector3.Dot(diffAsteroidVector, camera.transform.forward);
            if (angleCos <= 0)
            {
                GameManager.Instance.OnAsteroidPassed(gameObject);
            }
        }
    }
}
