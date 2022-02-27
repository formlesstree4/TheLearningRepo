using UnityEngine.SceneManagement;

namespace Assets.State
{
    public static class GameState
    {

        private static int currentGameLevel;
        private static bool isInitialized = false;
        private static bool isDead = false;

        public static int CurrentGameLevel => currentGameLevel;

        public static bool IsDead => isDead;

        public static void InitializeGameState()
        {
            if (isInitialized) return;
            currentGameLevel = 1;
            isInitialized = true;
        }

        public static void GoToNextLevel()
        {
            currentGameLevel++;
            SceneManager.LoadScene("Level Display", LoadSceneMode.Single);
        }

        public static void Died()
        {
            isDead = true;
            SceneManager.LoadScene("Level Display", LoadSceneMode.Single);
        }

        public static void Reset()
        {
            isDead = false;
        }

        public static void LoadGameScene()
        {
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }
    }
}
