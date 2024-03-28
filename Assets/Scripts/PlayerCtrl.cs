using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioClip itemSound;
    [SerializeField] private AudioClip fireSound;
    private AudioSource audioSource;

    private const float _maxSpeed = 4f;
    public float _speed;

    private const int _maxHealth = 3;
    public int _currentHealth;
    [SerializeField] private HealthCtrl healthBar;

    private const float _delayFire = 0.2f;
    private float _delayFireCD;

    [SerializeField] private GameObject shield;
    private const float _shieldTime = 3f;
    private float _shieldTimeCD;

    [SerializeField] private GameObject[] bullets;
    public int _bulletLevel;
    [SerializeField] private GameObject playerBullet;

    [SerializeField] private GameObject expAnim;

    public void LoadData(GameData data)
    {
        _currentHealth = data.health;
        transform.position = data.pos;
        _speed = data.speed;
        _bulletLevel = data.bulletLevel;

        playerBullet = bullets[_bulletLevel];
        healthBar.UpdateBar(_currentHealth, _maxHealth);
        gameObject.SetActive(true);
    }

    public void init()
    {
        _currentHealth = _maxHealth;
        transform.position = new Vector2(0, -4.5f);
        _speed = 3.5f;
        _bulletLevel = 0;

        playerBullet = bullets[_bulletLevel];
        healthBar.UpdateBar(_currentHealth, _maxHealth);
        gameObject.SetActive(true);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        _shieldTimeCD -= Time.deltaTime;
        _delayFireCD -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && _delayFireCD < 0)
            Fire();

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(x, y).normalized;
        Move(direction);
    }

    void Fire()
    {
        audioSource.PlayOneShot(fireSound);
        _delayFireCD = _delayFire;
        Instantiate(playerBullet, transform.position, Quaternion.identity);
    }

    void Move(Vector2 direction)
    {
        Vector2 minScene = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxScene = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        minScene.x += (gameObject.GetComponent<BoxCollider2D>().size.x / 2);
        maxScene.x -= (gameObject.GetComponent<BoxCollider2D>().size.x / 2);
        minScene.y += (gameObject.GetComponent<BoxCollider2D>().size.y / 2);
        maxScene.y -= (gameObject.GetComponent<BoxCollider2D>().size.y / 2);

        Vector2 pos = transform.position;
        pos += direction * _speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, minScene.x, maxScene.x);
        pos.y = Mathf.Clamp(pos.y, minScene.y, maxScene.y);

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") || col.CompareTag("EnemyBullet") || col.CompareTag("Meteorite"))
        {
            Destroy(col.gameObject);
            if (_shieldTimeCD <= 0)
            {
                _currentHealth--;
                healthBar.UpdateBar(_currentHealth, _maxHealth);
                if (_currentHealth <= 0)
                    EndGame();
                else
                    StartCoroutine(Blinking(gameObject, _shieldTime));
            }
        }
        else if (col.CompareTag("Boss"))
        {
            _currentHealth = 0;
            healthBar.UpdateBar(_currentHealth, _maxHealth);
            EndGame();
        }    
        else if (col.CompareTag("HP_Item"))
        {
            audioSource.PlayOneShot(itemSound);
            if (_currentHealth < _maxHealth)
                _currentHealth++;
            healthBar.UpdateBar(_currentHealth, _maxHealth);
        }
        else if (col.CompareTag("Dame_Item"))
        {
            audioSource.PlayOneShot(itemSound);
            if (_bulletLevel < bullets.Length - 1)
            {
                _bulletLevel++;
                playerBullet = bullets[_bulletLevel];
            }
        }
        else if (col.CompareTag("Speed_Item"))
        {
            audioSource.PlayOneShot(itemSound);
            if (_speed < _maxSpeed)
            {
                _speed+= 0.25f;
            }
        }
    }

    void EndGame()
    {
        Instantiate(expAnim, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        MapCtrl.Instance.SetGameManager(MapCtrl.GameState.Lose);
    }

    IEnumerator Blinking(GameObject obj, float blinkTime)
    {
        _shieldTimeCD = _shieldTime;
        bool isBlinking = false;
        while (blinkTime > 0)
        {
            shield.SetActive(true);
            isBlinking = !isBlinking;
            obj.GetComponent<SpriteRenderer>().color = isBlinking ? Color.black : Color.white;
            blinkTime -= Time.deltaTime;
            yield return null;
        }
        shield.SetActive(false);
        obj.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
