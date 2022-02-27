using Assets.Extensions;
using Assets.State;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{

    public int Enemies { get; private set; }
    public int Level = 0;
    public Camera gameCamera;
    public List<GameObject> enemies;

    private Text levelText;

    void Start()
    {
        levelText = GetComponentInChildren<Text>();
    }

    void Update()
    {
        levelText.text = string.Format("Lives: {0}", GameState.CurrentLives);
    }

    public void IncrementLevelAndLoad(float horizontalSpeed, float verticalSpeed)
    {
        Level = Assets.State.GameState.CurrentGameLevel;
        LoadCurrentLevel(horizontalSpeed, verticalSpeed);
    }

    void LoadCurrentLevel(float horizontalSpeed, float verticalSpeed)
    {
        var levelResourceData = (TextAsset)Resources.Load(string.Format("Levels\\{0}", Level), typeof(TextAsset));
        var itemsToRender = new List<string>();

        if (levelResourceData == null || levelResourceData.text.Length == 0)
        {
            Application.Quit();
            return;
        }

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
                    enemyCount++;
                }
                startingXPosition += cellWidth;
            }
            currentYPosition -= (cellWidth/1.5f);
        }

        Enemies = enemyCount;

    }

}