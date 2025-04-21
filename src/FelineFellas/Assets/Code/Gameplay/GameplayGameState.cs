using UnityEngine;

namespace FelineFellas
{
    public class GameplayGameState : IGameState
    {
        public void OnEnter(GameStateMachine stateMachine)
        {
            Debug.Log("you're in gameplay");
        }
    }
}