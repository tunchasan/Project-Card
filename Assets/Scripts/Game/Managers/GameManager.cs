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

        public void UpdateState(GameState state)
        {
            if (state != CurrentState)
            {
                CurrentState = state;
                
                OnGameStateChange?.Invoke(CurrentState);

                switch (state)
                {
                    case GameState.OnStart:
                        OnGameStart?.Invoke();
                        return;
                    case GameState.OnPlay:
                        OnGamePlay?.Invoke();
                        return;
                    case GameState.OnQuit:
                        OnGameQuit?.Invoke();
                        return;
                }
            }
        }
    }
}