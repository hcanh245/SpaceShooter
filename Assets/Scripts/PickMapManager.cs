using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickMapManager : MonoBehaviour
{
    private static PickMapManager _instance;
    public static PickMapManager Instance => _instance;

    [SerializeField] public string map;
    [SerializeField] public GameObject pickMapMenu;
    [SerializeField] public GameObject introGame;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        pickMapMenu.SetActive(true);
    }

    public void BackwardBtn_OnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator PlayGame()
    {
        pickMapMenu.SetActive(false);
        yield return StartCoroutine(introGame.GetComponent<Intro>().IntroGame());
        switch (map)
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
        yield return null;
    }

    public void Map1Btn_OnClick()
    {
        map = "1";
        StartCoroutine(PlayGame());
    }

    public void Map2Btn_OnClick()
    {
        map = "2";
        StartCoroutine(PlayGame());
    }

    public void Map3Btn_OnClick()
    {
        map = "3";
        StartCoroutine(PlayGame());
    }
}
