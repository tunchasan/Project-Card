using System;
using ProjectCard.Game.Utilities;
using UnityEngine;

namespace ProjectCard.Game.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public GameState CurrentState { get; private set; } = GameState.None;

        public static Action OnGameStart;
        public static Action OnGamePlay;
        public static Action OnGameQuit;
        public static Action<GameState> OnGameStateChange;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            OnGameStart?.Invoke();
        }

        public void QuitGame()
        {
            OnGameQuit?.Invoke();
            Application.Quit();
        }

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