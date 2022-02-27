using Assets.State;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{

    public float HorizontalMovementSpeed = 0.08f;

    public float VerticalMovementSpeed = 0.15f;

    private LevelController levelController;

    private int totalEnemies;

    private int currentEnemies;

    void Start()
    {
        levelController = GetComponent<LevelController>();
        levelController.IncrementLevelAndLoad(HorizontalMovementSpeed, VerticalMovementSpeed);
        SetEnemyCount();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // gather all active "enemy" prefabs
        var onScreenEnemies = GameObject.FindGameObjectsWithTag(Assets.Constants.ENEMY_TAG);
        var speedUpEnemies = false;

        if (onScreenEnemies.Length == 0)
        {
            GameState.GoToNextLevel();
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
            var percentage = ((float)currentEnemies / totalEnemies);
            for (var i = 0; onScreenEnemies.Length > i; i++)
            {
                // we need to calculate the ratio of the frameSkip
                // basically, we have a minimum, current, and maximum frameSkip
                // so we need to calculate a "percentage" value of sorts
                // where we get a number between min and max.
                // f(x) = min + (max - min) * x
                var enemy = onScreenEnemies[i].GetComponent<RegularEnemyBehavior>();
                var newDelay = enemy.animationAndMovement.endingSpeed + (enemy.animationAndMovement.startingSpeed - enemy.animationAndMovement.endingSpeed) * percentage;
                enemy.animationAndMovement.secondsBetweenMovement = newDelay;
            }
        }

    }

    void SetEnemyCount()
    {
        currentEnemies = levelController.Enemies;
        totalEnemies = currentEnemies;
    }

}
