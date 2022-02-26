using Assets.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public int Enemies { get; private set; }
    public int Level = 0;
    public Camera gameCamera;
    public List<GameObject> enemies;

    public void IncrementLevelAndLoad(float horizontalSpeed, float verticalSpeed, int frameDelay, int minFrameDelay)
    {
        Level = Assets.State.GameState.CurrentGameLevel;
        LoadCurrentLevel(horizontalSpeed, verticalSpeed, frameDelay, minFrameDelay);
    }

    void LoadCurrentLevel(float horizontalSpeed, float verticalSpeed, int frameDelay, int minFrameDelay)
    {
        var levelResourceData = (TextAsset)Resources.Load(string.Format("Levels\\{0}", Level), typeof(TextAsset));
        var itemsToRender = new List<string>();
        itemsToRender.AddRange(levelResourceData.text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
        
        var maxRowSize = itemsToRender.Max(f => f.Length);

        // Now that we have the maximum row size, we need to divide the screen
        // into a grid that fits this many items along it. This is actually a
        // relatively difficult feat
        var cameraBounds = gameCamera.OrthographicBounds();

        var farLeft = cameraBounds.min.x;
        var farRight = cameraBounds.max.x;
        var upperLeft = cameraBounds.max.y;
        var length = Mathf.Abs(farLeft) + Mathf.Abs(farRight);
        var cellWidth = length / (maxRowSize + 1);
        var currentYPosition = upperLeft - (cellWidth/4);
        var enemyCount = 0;

        foreach (var row in itemsToRender)
        {
            var startingXPosition = farLeft + cellWidth;
            foreach(var cell in row)
            {
                if (cell != ' ')
                {
                    var enemyIndex = cell - '0';
                    var enemy = Instantiate(enemies[enemyIndex]);
                    enemy.transform.position = new Vector3(startingXPosition, currentYPosition);
                    enemy.tag = Assets.Constants.ENEMY_TAG;
                    var movement = enemy.GetComponent<SimpleAnimatorAndMovement>();
                    movement.horizontalMovementSpeed = horizontalSpeed;
                    movement.verticalMovementSpeed = verticalSpeed;
                    movement.frameDelay = frameDelay;
                    movement.minimumFrameDelay = minFrameDelay;
                    enemyCount++;
                }
                startingXPosition += cellWidth;
            }
            currentYPosition -= (cellWidth/2);
        }

        Enemies = enemyCount;

    }

}