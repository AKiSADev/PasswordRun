using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseResume : MonoBehaviour
{

    public Player player;
    public GameObject panel;
    public Text passwordText;

    public void PauseGame()
    {
        player.setIsPaused(true);
        panel.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        panel.gameObject.SetActive(false);
        player.setIsPaused(false);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Debug.Log("STO CERCANDO DI CARICARE LA HOME");
        SceneManager.LoadScene("MainMenu");
    }

    public void CopyOnClipboard()
    {
        Debug.Log("COPIED");
        GUIUtility.systemCopyBuffer = passwordText.text;
    }

    public void goToLeaderBoard()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LeaderBoard", LoadSceneMode.Additive);

    }
}
