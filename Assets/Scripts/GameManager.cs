using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : BaseCtrl
{ 
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    public GameObject continueBtn;
    public GameData data;
    public bool isContinue;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        isContinue = false;
        data = LoadData();
        CheckContinue();
    }

    void CheckContinue()
    {
        if (data != null)
            continueBtn.GetComponent<Button>().interactable = true;
        else continueBtn.GetComponent<Button>().interactable = false;
    }

    public void ContinueBtn_OnClick()
    {
        switch (data.map)
        {
            case "1":
                break;
            case "2":
                break;
            case "3":
                break;
        }
        isContinue = true;
    }


    public void StartBtn_OnClick()
    {
        SceneManager.LoadScene("PickMap");
        isContinue = false;
    }

    public void GuideBtn_OnClick()
    {
        SceneManager.LoadScene("Guide");
    }

    public void StoryBtn_OnClick()
    {
        SceneManager.LoadScene("Story");
    }

    public void QuitBtn_OnClick()
    {
        Application.Quit();
    } 
}



