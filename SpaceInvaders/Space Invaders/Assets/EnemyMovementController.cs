using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{

    public int MaximumFrameDelay = 240;

    public int MinimumFrameDelay = 80;

    public float HorizontalMovementSpeed = 0.08f;

    public float VerticalMovementSpeed = 0.15f;

    private LevelController levelController;

    private int totalEnemies;

    private int currentEnemies;


    void Start()
    {
        levelController = GetComponent<LevelController>();
        levelController.IncrementLevelAndLoad(HorizontalMovementSpeed, VerticalMovementSpeed, MaximumFrameDelay, MinimumFrameDelay);
        SetEnemyCount();
    }

    // Update is called once per frame
    void Update()
    {

        // gather all active "enemy" prefabs
        var onScreenEnemies = GameObject.FindGameObjectsWithTag(Assets.Constants.ENEMY_TAG);
        var speedUpEnemies = false;

        if (onScreenEnemies.Length == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level Display", UnityEngine.SceneManagement.LoadSceneMode.Single);
            // SetEnemyCount();
            return;
        }

        if (onScreenEnemies.Length < currentEnemies)
        {
            currentEnemies = onScreenEnemies.Length;
            speedUpEnemies = true;
        }

        // see if we need to toggle movement
        var toggle = false;

        for (var i = 0; onScreenEnemies.Length > i; i++)
        {
            var enemy = onScreenEnemies[i].GetComponent<RegularEnemyBehavior>();
            if (enemy.shouldToggleMovement)
            {
                toggle = true;
                break;
            }
        }

        if (toggle)
        {
            for (var i = 0; onScreenEnemies.Length > i; i++)
            {
                var enemy = onScreenEnemies[i].GetComponent<RegularEnemyBehavior>();
                enemy.animationAndMovement.SwitchMovementDirections();
                enemy.ResetToggle();
            }
        }

        if (speedUpEnemies)
        {
            var percentage = ((float)currentEnemies / (float)totalEnemies);
            var newDelay = MinimumFrameDelay + Mathf.RoundToInt((MaximumFrameDelay - (float)MinimumFrameDelay) * percentage);
            if (percentage < 0.5)
            {
                newDelay = Mathf.FloorToInt((float)newDelay + (float)newDelay * 0.3f);
            }

            for (var i = 0; onScreenEnemies.Length > i; i++)
            {
                // we need to calculate the ratio of the frameSkip
                // basically, we have a minimum, current, and maximum frameSkip
                // so we need to calculate a "percentage" value of sorts
                // where we get a number between min and max.
                // f(x) = min + (max - min) * x

                var enemy = onScreenEnemies[i].GetComponent<RegularEnemyBehavior>();
                enemy.animationAndMovement.frameDelay = newDelay;
            }
        }

    }

    void SetEnemyCount()
    {
        currentEnemies = levelController.Enemies;
        totalEnemies = currentEnemies;
    }

}
