using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DataStorage : MonoBehaviour
{

    private string password = "";
    public int points = 0;

    public GameObject panel;
    public GameObject deathPanel;

    private void Start()
    {
        panel.gameObject.SetActive(false);
        deathPanel.gameObject.SetActive(false);
    }

    public void updatePointsAndPassword(string character)
    {
        updatePoints();
        updatePassword(character);
    }

    private void updatePoints()
    {
        points = points + 10;
        this.gameObject.transform.Find("Canvas").transform.Find("Points").GetComponent<Text>().text = points.ToString();
    }

    private void updatePassword(string character)
    {
        switch (character)
        {
            case "doubleQuote" :
                character = "\"";
                break;

            case "duePunti":
                character = ":";
                break;

            case "questionMark":
                character = "?";
                break;
        }

        if (password.Length < 16)
            password = password + character;

        Debug.Log("Password: " + password);
    }

    public static int CheckStrength(string password)
    {
        int score = 1;

        if (password.Length < 1)
            return 0;
        if (password.Length < 4)
            return 1;

        if (password.Length >= 8)
            score++;
        if (password.Length >= 12)
            score++;
        if (Regex.Match(password, @"/\d+/", RegexOptions.ECMAScript).Success)
            score++;
        if (Regex.Match(password, @"/[a-z]/", RegexOptions.ECMAScript).Success &&
            Regex.Match(password, @"/[A-Z]/", RegexOptions.ECMAScript).Success)
            score++;
        if (Regex.Match(password, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript).Success)
            score++;

        return score;
    }

    public string getPassword()
    {
        return password;
    }


}
