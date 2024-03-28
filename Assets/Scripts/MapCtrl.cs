using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MapCtrl : BaseCtrl
{
    private static MapCtrl _instance;
    public static MapCtrl Instance => _instance;

    public GameObject playMenu;
    public GameObject pauseMenu;
    public GameObject pauseBtn;
    private int _score;
    private GameState gameState;
    private GameData _data;
    public UnityEvent<int> updateScore;
    public GameObject player;
    public GameObject spawner;
    [SerializeField] private GameObject WinImg;
    [SerializeField] private GameObject LoseImg;

    public enum GameState
    {
        Playing,
        Pause,
        Win,
        Lose
    }

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        if (GameManager.Instance.isContinue)
        {
            _data = GameManager.Instance.data;
            player.GetComponent<PlayerController>().LoadData(_data);
            spawner.GetComponent<ObjSpawner>()._turn = _data.turn;
            SetScore(_data.score);
        }
        else
        {
            player.GetComponent<PlayerController>().init();
            spawner.GetComponent<ObjSpawner>()._turn = 1;
        }    
           
        GameManager.Instance.data = null;
        SaveData(null);

        _score = 0;

        gameState = GameState.Playing;
        SetGameManager(gameState);
    }

    public void SetScore(int score)
    {
        _score += score;
        updateScore?.Invoke(_score);
    }

    void UpdateGameState()
    {
        switch (gameState)
        {
            case GameState.Playing:
                Time.timeScale = 1;
                playMenu.SetActive(true);
                pauseMenu.SetActive(false);
                pauseBtn.SetActive(true);
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                playMenu.SetActive(false);
                pauseMenu.SetActive(true);
                pauseBtn.SetActive(false);
                break;
            case GameState.Win:
                WinImg.SetActive(true);
                pauseBtn.SetActive(false);
                SetScore(player.GetComponent<PlayerController>()._currentHealth * 1000);
                StartCoroutine(LoadEndGame());
                break;
            case GameState.Lose:
                LoseImg.SetActive(true);
                pauseBtn.SetActive(false);
                StartCoroutine(LoadEndGame());
                break;
        }
    }

    public void SetGameManager(GameState state)
    {
        gameState = state;
        UpdateGameState();
    }

    public void PauseBtn_OnClick()
    {
        SetGameManager(GameState.Pause);
    }

    public void ResumeBtn_OnClick()
    {
        SetGameManager(GameState.Playing);
    }

    public void MenuBtn_OnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartBtn_OnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SaveBtn_OnClick()
    {
        SaveData(_data);
        MenuBtn_OnClick();
    }

    public void CheckPoint()
    {
        _data = new GameData();
        _data.turn = spawner.GetComponent<ObjSpawner>()._turn;
        _data.score = _score;
        _data.map = PickMapManager.Instance.map;
        _data.health = player.GetComponent<PlayerController>()._currentHealth;
        _data.pos = player.GetComponent<Transform>().transform.position;
        _data.speed = player.GetComponent<PlayerController>()._speed;
        _data.bulletLevel = player.GetComponent<PlayerController>()._bulletLevel;
    }

    IEnumerator LoadEndGame()
    {
        player.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(MoveToPosition(player, new Vector2(player.transform.position.x, player.transform.position.y + 10), 3f));
        SceneManager.LoadScene("EndGame");
    }
}
