using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RenderLevelDetails : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Assets.State.GameState.CurrentGameLevel += 1;
        GetComponentInChildren<Text>().text = string.Format("Level {0}", Assets.State.GameState.CurrentGameLevel);
        StartCoroutine(ChangeToLevelScene());
    }

    IEnumerator ChangeToLevelScene()
    {
        yield return new WaitForSecondsRealtime(4.0f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

}
