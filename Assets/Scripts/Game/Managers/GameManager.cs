using System;
using ProjectCard.Game.Utilities;

namespace ProjectCard.Game.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public GameState CurrentState { get; private set; } = GameState.None;

        public static Action OnGameStart;
        public static Action OnGamePlay;
        public static Action OnGameQuit;
        public static Action<GameState> OnGameStateChange;
        
        private void Start()
        {
            CurrentState = GameState.OnStart;
            OnGameStart?.Invoke();
        }
    }
}