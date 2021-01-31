using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Script
{
    public class GameManager : Singleton<GameManager>
    {
        public static float TransitionFadeSpeed => 1f;
        public delegate void Transition(bool isLight);
        public event Transition OnTransition;
        public event Transition AfterTransition;
        public GameState State { get; private set; }
        public string PlayerName { get; set; }

        private int _currentLevelIndex = 1;
        private int _lastLevelIndex = 0;

        private static bool _hasLoadedOnce = false;

        public GameManager()
        {
            State = GameState.Init;
            
            // Ik doe hier de aanname dat You een beetje oke gaat lezen in zinnen
            PlayerName = "You";
        }

        private void OnEnable()
        {
            if (!_hasLoadedOnce) LoadMenu();
            _hasLoadedOnce = true;
        }

        private void LoadMenu()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }

        public void ResetGame()
        {
            _currentLevelIndex = 1;
            _lastLevelIndex = 3;
        }

        public void ChangeState(GameState desiredState)
        {
            var lastState = State;
            OnTransition?.Invoke(true);

            switch (desiredState)
            {
                case GameState.GameFailed:
                    Debug.Log("LOSER!");
                    _currentLevelIndex = 3;
                    _lastLevelIndex = 2;
                    break;
                case GameState.GameWon:
                    Debug.Log("WINNER!");
                    _currentLevelIndex = 3;
                    _lastLevelIndex = 2;
                    break;
                case GameState.Init:
                    ResetGame();
                    break;
                case GameState.IntoSequence:
                    _currentLevelIndex = 2;
                    _lastLevelIndex = 1;
                    break;
                case GameState.GamePlay:
                    _currentLevelIndex = 2;
                    _lastLevelIndex = 1;

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(desiredState), desiredState, null);
            }
            
           Invoke(nameof(LoadNewLevel), TransitionFadeSpeed + 0.1f);
        }

        private void LoadNewLevel()
        {
            SceneManager.sceneLoaded += OnLoaded;
            SceneManager.sceneUnloaded += AfterLoaded;
            SceneManager.LoadSceneAsync(_currentLevelIndex, LoadSceneMode.Additive);
        }

        private void OnLoaded(Scene scene, LoadSceneMode mode)
        {
            if (_lastLevelIndex > 0) 
                SceneManager.UnloadSceneAsync(_lastLevelIndex);

        }

        private void AfterLoaded(Scene scene)
        {
            // Scene 3 is de laatste dus gebruiken we een donkere overgang
            AfterTransition?.Invoke(scene.buildIndex != 3);
        }
    }
}