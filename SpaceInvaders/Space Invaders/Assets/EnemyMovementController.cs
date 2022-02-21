using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{

    public float enemyDelayDecreasePerKill = 0.08f;

    private int totalEnemies;

    private int currentEnemies;


    void Start()
    {
        SetEnemyCount();
    }

    // Update is called once per frame
    void Update()
    {

        // gather all active "enemy" prefabs
        var onScreenEnemies = FindObjectsOfType<RegularEnemyBehavior>();
        var speedUpEnemies = false;

        if (onScreenEnemies.Length < currentEnemies)
        {
            currentEnemies = onScreenEnemies.Length;
            speedUpEnemies = true;
        }

        // see if we need to toggle movement
        var toggle = false;

        for (var i = 0; onScreenEnemies.Length > i; i++)
        {
            var enemy = onScreenEnemies[i];
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
                var enemy = onScreenEnemies[i];
                enemy.animationAndMovement.SwitchMovementDirections();
                enemy.ResetToggle();
            }
        }

        if (speedUpEnemies)
        {
            for (var i = 0; onScreenEnemies.Length > i; i++)
            {
                var enemy = onScreenEnemies[i];
                //enemy.animationAndMovement.frameDelay = 
            }
        }

    }

    void SetEnemyCount()
    {
        currentEnemies = FindObjectsOfType<RegularEnemyBehavior>().Length;
        totalEnemies = currentEnemies;
    }


}
