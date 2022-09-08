using UnityEngine.SceneManagement;

namespace ProjectCard.Game.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void LoadMenuScene()
        {
            SceneManager.LoadScene(0);
        }
        
        public void LoadGameScene()
        {
            SceneManager.LoadScene(1);
        }
    }
}