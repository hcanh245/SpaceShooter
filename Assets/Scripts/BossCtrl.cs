using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BossCtrl : BaseCtrl
{
    [SerializeField] private EnemyInfo bossInfo;
    private Vector2[] targetPoint;
    [SerializeField] private GameObject bossBulletPos;
    private GameObject bossBullet;
    private GameObject expAnim;
    private int _maxHealth;
    private int _curHealth;
    private int _score;
    private GameObject healthBar;
    private GameObject player;

    private void Awake()
    {
        targetPoint = new Vector2[]
        {
            new Vector2(0, 3.75f),
            new Vector2(2.25f, 3.75f),
            new Vector2(-2.25f, 3.75f)
        };
    }

    void UpdateInfoBoss()
    {
        _maxHealth = bossInfo.health;
        _curHealth = _maxHealth;
        _score = bossInfo.score;
        bossBullet = bossInfo.bullet;
        expAnim = bossInfo.exp;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthBar = GameObject.FindGameObjectWithTag("HealthBarBoss");
        UpdateInfoBoss();
        StartCoroutine(BossAI());
    }

    public void TakeDamage(int damage)
    {
        if (_curHealth >= damage)
            _curHealth -= damage;
        else
            _curHealth = 0;
        healthBar.GetComponent<HealthCtrl>().UpdateBar(_curHealth, _maxHealth);
        if (_curHealth <= 0)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        DestroyEnemy(_score, expAnim, gameObject);
        MapCtrl.Instance.SetGameManager(MapCtrl.GameState.Win);
    }

    void Attack()
    {
        Instantiate(bossBullet, bossBulletPos.transform.position, Quaternion.identity);
    }

    IEnumerator BossAI()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return StartCoroutine(MoveToPosition(gameObject, targetPoint[0], 3f));
        healthBar.GetComponent<HealthCtrl>().UpdateBar(_curHealth, _maxHealth);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        InvokeRepeating("Attack", 1f, 1f);
        yield return new WaitForSeconds(3f);
        while (true)
        {
            yield return StartCoroutine(MoveBetweenPositions(targetPoint[0], targetPoint[1], 3f));
            yield return StartCoroutine(MoveBetweenPositions(targetPoint[0], targetPoint[2], 3f));
            yield return StartCoroutine(MoveBetweenPositions(targetPoint[0], player.transform.position, 6f));
        }
    }
}
