using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    string playerName;

    public GameObject namePanel;
    public GameObject usedPanel;
    public GameObject text;

    private void Start()
    {
        playerName = PlayerPrefs.GetString("PlayerName");

        Debug.Log("PLAYER NAME: " + playerName);

        if (playerName == null || playerName.Equals(""))
            namePanel.SetActive(true);

    }

    public void goToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }


    public void goToLeaderBoard()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LeaderBoard", LoadSceneMode.Additive);

    }


    public void goToExpanation()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Explanation");
    }

    public void saveName()
    {
        //TODO: check che il nome non sia utilizzato
        string name = text.GetComponent<UnityEngine.UI.Text>().text;

 //       HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://password-run.herokuapp.com/passwordrun/names" + name);
 //       HttpWebResponse response = (HttpWebResponse)request.GetResponse();

 //       if (response.StatusCode == HttpStatusCode.OK)
//        {
            PlayerPrefs.SetString("PlayerName", name);
            PlayerPrefs.Save();
            namePanel.SetActive(false);
//        }
//        else
 //       {
 //           usedPanel.SetActive(true);
 //       }


        Debug.Log("PlayerName: " + PlayerPrefs.GetString("PlayerName"));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void closeUsedPanel()
    {
        usedPanel.SetActive(false);
    }

}
