using System;
using System.ComponentModel;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public enum GameState
    {
        StartMenu,
        Play,
        Pause,
        GameOver
    }

    public class GameManager
    {
        public static readonly GameManager Instance = new GameManager();

        public float GameStartTime { get; private set; }

        private GameState gameState = GameState.StartMenu;
        /// <summary>
        /// Gets current game state.
        /// </summary>
        public GameState GameState
        {
            get => gameState;
            set
            {
                if (gameState == value)
                    return;
                if (value == GameState.Play && gameState != GameState.Pause)
                {
                    GameStartTime = Time.time;
                    Score = 0;
                    PassedAsteroids = 0;
                }
                gameState = value;
                if (gameState == GameState.GameOver)
                {
                    SteamHelper.WriteHighScore(HighScore);
                }
         
                OnPropertyChanged(nameof(GameState));
            }
        }

        /// <summary>
        /// Gets current game speed modifier.
        /// </summary>
        public float SpeedModifier => IsHighSpeed ? 2f : 1f;

        private int score = 0;
        /// <summary>
        /// Gets current game session score.
        /// </summary>
        public int Score
        {
            get => score;
            set
            {
                if (score == value)
                    return;
                score = value;
                if (score > highScore)
                {
                    HighScore = score;
                }
                OnPropertyChanged(nameof(Score));
            }
        }

        private int passedAsteroids;

        /// <summary>
        /// Gets count of passed asteroids.
        /// </summary>
        public int PassedAsteroids
        {
            get => passedAsteroids;
            set
            {
                if (passedAsteroids == value)
                    return;
                passedAsteroids = value;
                OnPropertyChanged(nameof(PassedAsteroids));
            }
        }


        private int highScore = 0;
        /// <summary>
        /// Gets current game session high score.
        /// </summary>
        public int HighScore
        {
            get => highScore;
            set
            {
                if (highScore == value)
                    return;
                highScore = value;
                OnPropertyChanged(nameof(HighScore));
            }
        }


        /// <summary>
        /// Gets if high speed is enabled.
        /// </summary>
        public bool IsHighSpeed { get; set; }

        /// <summary>
        /// The event is invoked on any property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The event is invoked on asteroid pass.
        /// </summary>
        public event Action<GameObject> AsteroidPass;

        public GameManager()
        {
           HighScore = SteamHelper.ReadHighScore();
        }

        /// <summary>
        /// The method should be called whenever the asteroid has been passed.
        /// </summary>
        /// <param name="asteroid">Asteroid that was passed.</param>
        public void OnAsteroidPassed(GameObject asteroid)
        {
            if (asteroid == null)
            {
                throw new NullReferenceException(nameof(asteroid));
            }
            Score += IsHighSpeed ? 2 : 1;
            PassedAsteroids++;

            AsteroidPass?.Invoke(asteroid);
        }

        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        
    }
}
