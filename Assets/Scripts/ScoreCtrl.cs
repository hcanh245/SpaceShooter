using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class ScoreCtrl : MonoBehaviour
{
    private TextMeshProUGUI _textScore;
    public int highscore;
    public string key = "HighScore";

    void Start()
    {
        highscore = PlayerPrefs.GetInt(key + PickMapManager.Instance.map, 0);
        _textScore = GetComponent<TextMeshProUGUI>();
        MapCtrl.Instance.updateScore?.AddListener(UpdateScore);
        MapCtrl.Instance.SetScore(0);
    }

    void UpdateScore(int score)
    {
        string scoreStr = string.Format("{0:0000000}", score);
        _textScore.text = scoreStr;
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt(key + PickMapManager.Instance.map, highscore);
            PlayerPrefs.Save();
        }
            
    }
}
