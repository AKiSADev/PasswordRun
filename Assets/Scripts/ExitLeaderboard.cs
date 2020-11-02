using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLeaderboard : MonoBehaviour
{
    public void ExitThisScene()
    {
        Debug.Log("Exit");
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("LeaderBoard");
    }
}
