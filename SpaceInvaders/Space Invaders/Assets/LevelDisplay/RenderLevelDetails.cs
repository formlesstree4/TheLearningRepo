using Assets.State;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RenderLevelDetails : MonoBehaviour
{

    void Start()
    {
        GameState.InitializeGameState();
        if (GameState.IsDead)
        {
            GameState.Reset();
        }
        GetComponentInChildren<Text>().text = string.Format("Level {0}", Assets.State.GameState.CurrentGameLevel);
        StartCoroutine(ChangeToLevelScene());
    }

    IEnumerator ChangeToLevelScene()
    {
        yield return new WaitForSecondsRealtime(4.0f);
        GameState.LoadGameScene();
    }

}
