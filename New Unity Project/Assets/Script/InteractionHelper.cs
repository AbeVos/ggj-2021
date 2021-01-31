using UnityEngine;

namespace Script
{
    public class InteractionHelper : MonoBehaviour
    {
        public void StartGame()
        {
            GameManager.Instance.ChangeState(GameState.IntoSequence);
        }
    }
}