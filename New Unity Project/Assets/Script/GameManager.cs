using System;
using UnityEngine;
using UnityEngine.Events;

namespace Script
{
    public class GameManager : Singleton<GameManager>
    {

        public delegate void Transition(bool isLight);
        public event Transition OnTransition;
        public event Transition AfterTransition;
        public GameState State { get; private set; }
        public string PlayerName { get; set; }

        public GameManager()
        {
            State = GameState.Init;
            
            // Ik doe hier de aanname dat You een beetje oke gaat lezen in zinnen
            PlayerName = "You";
        }

        public void ChangeState(GameState desiredState)
        {
            var lastState = State;
            
            Debug.Log($"OP NAAR STATE {desiredState}. Mijn vorige state was {lastState}");
            OnTransition?.Invoke(true);

            switch (desiredState)
            {
                case GameState.GameFailed:
                    Debug.Log("LOSER!");
                    break;
                case GameState.GameWon:
                    Debug.Log("WINNER!");
                    break;
                case GameState.Init:
                    break;
                case GameState.IntoSequence:
                    break;
                case GameState.GamePlay:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(desiredState), desiredState, null);
            }
        }
    }
}