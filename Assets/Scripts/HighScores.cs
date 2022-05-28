using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using TMPro;
//using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;


public class HighScores : MonoBehaviour
{
    const string privateCode = "WP5LjYuNkU-KCoIwVQz6Rg6xoFFwyV7EOkc4pFIZzsSA";
    const string publiCode = "5ffb7b74d15893198881deb9";
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highscoresList;
    public TextMeshProUGUI[] usernames;
    public TextMeshProUGUI[] scores;
    public TMP_InputField inputField;
    public GameObject usernameTaken;
    public GameMaster gm;

    public IEnumerator UploadNewScore(string username, int score)
    {
        UnityWebRequest www = new UnityWebRequest(webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(username) + "/" + score);

        yield return www.SendWebRequest();

        if (string.IsNullOrEmpty(www.error))
        {

            Debug.Log("uploaded");
        }
        else
        {
            Debug.Log("error" + www.error);
        }
    }

    public IEnumerator DownloadHighscoresFromDB()
    {
        UnityWebRequest www = new UnityWebRequest(webURL + publiCode + "/pipe/0/10");
        DownloadHandlerBuffer dH = new DownloadHandlerBuffer();
        www.downloadHandler = dH;

        yield return www.SendWebRequest();

        if (string.IsNullOrEmpty(www.error))
        {
            FormatScores(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("error" + www.error);
        }
    }

    void FormatScores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];
        //Debug.Log(textStream);
        for(int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, score);
            usernames[i].text = username;
            scores[i].text = score.ToString();
        }
    }

    public bool CheckUsername()
    {
        string newUsername = inputField.text.Trim();

        Debug.Log(highscoresList.Length);

        for (int i = 0; i < highscoresList.Length; i++)
        {
            string temp = highscoresList[i].username;
            temp = Regex.Replace(temp, @"\u00A0", "");
            temp.Trim();
            temp.Replace(" ", "");
            temp.Replace("/n", "");
            temp = Regex.Replace(temp, "[^\\w\\._]", "");

            if (newUsername.Length != temp.Length)
            {
                Debug.Log("temp: " + temp);
                Debug.Log("usermane: " + newUsername);
            }

            Debug.Log(string.Compare(temp, newUsername));

            if(string.Compare(temp, newUsername) == 0)
            {
                usernameTaken.SetActive(true);
                return true;
            }
        }
        usernameTaken.SetActive(false);

        return false;
    }
}

[Serializable]
public struct Highscore
{
    public string username;
    public int score;

    public Highscore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }
}

