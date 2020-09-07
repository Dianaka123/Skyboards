using System.Collections.Generic;
using System.ComponentModel;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Asteroid
{
    public class AsteroidGenerator : ResetableBehaviour, IObjectPoolFactory<GameObject>
    {
        private const int ZoneCount = 20;
        private const int ZoneFrame = 3;

        private readonly FrameRandom frameRandom = new FrameRandom(ZoneCount, ZoneFrame);
        private readonly List<GameObject> createdAsteroids = new List<GameObject>();
        private ObjectPool<GameObject> asteroidPool;

        private float interval = 0;

        public float Min = -2.5f;
        public float Max = 2.5f;
        
        public GameObject Asteroid;

        protected override void Start()
        {
            base.Start();
            asteroidPool = new ObjectPool<GameObject>(this);
            GameManager.Instance.AsteroidPass += AsteroidHelperAsteroidPass;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GameManager.Instance.AsteroidPass -= AsteroidHelperAsteroidPass;
        }

        private void AsteroidHelperAsteroidPass(GameObject obj) => ReleaseAsteroid(obj);

        private void ReleaseAsteroid(GameObject obj)
        {
            obj.SetActive(false);
            createdAsteroids.Remove(obj);
            asteroidPool.Release(obj);
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.GameState != GameState.Play)
            {
                return;
            }

            // Compute span interval based on passed asteroids count
            var spawnInterval = Mathf.Max(2.0f - GameManager.Instance.PassedAsteroids * 0.1f, 0.5f);
            if (interval >= spawnInterval)
            {
                var randomIndex = frameRandom.Generate();
                // Map random index onto x range
                var x = Min + (Max - Min) * ((float)randomIndex / (ZoneCount - 1));

                // Spawn asteroid using object pool
                var asteroid = asteroidPool.Capture();
                asteroid.SetActive(true);
                createdAsteroids.Add(asteroid);

                asteroid.transform.position = new Vector3(x, 19, 113);
                interval %= spawnInterval;
            }
            interval += Time.deltaTime;
        }

        public GameObject Create()
        {
            var asteroid = Instantiate(Asteroid);
            asteroid.SetActive(false);
            return asteroid;
        }

        protected override void OnReset()
        {
            var listCopy = createdAsteroids.ToArray();
            foreach (var asteroid in listCopy)
            {
                ReleaseAsteroid(asteroid);
            }
            interval = 0;
        }
    }
}
