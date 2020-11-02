using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoresReader : MonoBehaviour
{
    public List<GameObject> all;
    public List<GameObject> thisp;


    private string playerName;

    void Start()
    {
        playerName = PlayerPrefs.GetString("PlayerName");
        //playerName = "UnityTester2";

        HttpWebRequest requestAll = (HttpWebRequest) WebRequest.Create("https://password-run.herokuapp.com/passwordrun/scores");
        HttpWebResponse responseAll = (HttpWebResponse)requestAll.GetResponse();
        StreamReader readerAll = new StreamReader(responseAll.GetResponseStream());
        string jsonResponseAll = readerAll.ReadToEnd();
        jsonResponseAll = "{ \"scores\": " + jsonResponseAll + "}";
        PlayerScores psa = JsonUtility.FromJson<PlayerScores>(jsonResponseAll);


        HttpWebRequest requestThis = (HttpWebRequest)WebRequest.Create("https://password-run.herokuapp.com/passwordrun/scores/" + playerName);
        HttpWebResponse responseThis = (HttpWebResponse)requestThis.GetResponse();
        StreamReader readerThis = new StreamReader(responseThis.GetResponseStream());
        string jsonResponseThis = readerThis.ReadToEnd();
        jsonResponseThis = "{ \"scores\": " + jsonResponseThis + "}";
        PlayerScores pst = JsonUtility.FromJson<PlayerScores>(jsonResponseThis);

        IstantiateAll(psa);
        IsantiateThis(pst);


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ExitThisScene();
    }

    public void ExitThisScene()
    {
        SceneManager.UnloadSceneAsync("LeaderBoard");
    }

    private void IsantiateThis(PlayerScores pst)
    {
        int max;

        if (pst.scores.Count > 5)
        {
            max = 5;
        }
        else
        {
            max = pst.scores.Count;
        }


        for (int i = 0; i < max; i++)
        {
            thisp[i].GetComponent<Text>().text = (i + 1) + ". " + pst.scores[i].playerName + "  " + pst.scores[i].points;
            thisp[i].SetActive(true);
        }
    }

    private void IstantiateAll(PlayerScores psa)
    {
        int max;

        if (psa.scores.Count > 5)
        {
            max = 5;
        }
        else
        {
            max = psa.scores.Count;
        }

        for (int i = 0; i < max; i++)
        {
            all[i].GetComponent<Text>().text = (i + 1) + ". " + psa.scores[i].playerName + "  " + psa.scores[i].points;
            all[i].SetActive(true);
        }
    }

    [Serializable]
    private class PlayerScores
    {
        public List<Score> scores;
    }

    [Serializable]
    private class Score
    {
        public int id;
        public string playerName;
        public int points;
        public string date;
    }



}
