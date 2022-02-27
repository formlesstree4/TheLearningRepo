using UnityEngine.SceneManagement;

namespace Assets.State
{
    public static class GameState
    {

        private static int lives = 3;
        private static int currentGameLevel;
        private static bool isInitialized = false;
        private static bool isDead = false;

        public static int CurrentGameLevel => currentGameLevel;

        public static bool IsDead => isDead;

        public static int CurrentLives => lives;

        public static void InitializeGameState()
        {
            if (isInitialized) return;
            currentGameLevel = 1;
            isInitialized = true;
        }

        public static void GoToNextLevel()
        {
            currentGameLevel++;
            LoadLevelDisplay();
        }

        public static void Died()
        {
            if (!isInitialized) return;
            if (isDead) return;
            isDead = true;
            lives--;
            if (lives == 0)
            {
                LoadGameOver();
            }
            else
            {
                LoadLevelDisplay();
            }
        }

        public static void Reset()
        {
            isDead = false;
        }

        public static void LoadGameScene()
        {
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }

        public static void LoadGameOver()
        {
            SceneManager.LoadScene("Game Over", LoadSceneMode.Single);
        }

        public static void LoadLevelDisplay()
        {

        }

    }
}
