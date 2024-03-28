using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class EndGameManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public string key = "HighScore";

    void Start()
    {
        highScoreText.text = string.Format("{0:0000000}", PlayerPrefs.GetInt(key + PickMapManager.Instance.map, 0));
    }

    public void RestartBtn_OnClick()
    {
        switch (PickMapManager.Instance.map)
        {
            case "1":
                SceneManager.LoadScene("Map1");
                break;
            case "2":
                SceneManager.LoadScene("Map2");
                break;
            case "3":
                SceneManager.LoadScene("Map3");
                break;
        }
    }

    public void MenuBtn_OnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
