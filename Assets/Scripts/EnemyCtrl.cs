using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyCtrl : BaseCtrl
{
    [SerializeField] private EnemyInfo enemyInfo;
    private GameObject enemyBullet;
    private GameObject expAnim;
    private int _curHealth;
    private int _score;

    private void Awake()
    {
        _curHealth = enemyInfo.health;
        _score = enemyInfo.score;
        enemyBullet = enemyInfo.bullet;
        expAnim = enemyInfo.exp;
    }

    void Start()
    {
        StartCoroutine(EnemyMove());
    }

    void Attack()
    {
        Instantiate(enemyBullet, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            DestroyEnemy(_score, expAnim, gameObject);
    }

    public void TakeDamage(int damage)
    {
        _curHealth -= damage;
        if (_curHealth <= 0)
            DestroyEnemy(_score, expAnim, gameObject);
    }

    IEnumerator EnemyMove()
    {
        Vector2 pos = transform.position;
        pos.y -= 3f;
        yield return StartCoroutine(MoveToPosition(gameObject, pos, 3f));
        InvokeRepeating("Attack", 1f, Random.Range(2f, 3f));
        yield return new WaitForSeconds(2f);
        pos.y -= 8f;
        yield return StartCoroutine(MoveToPosition(gameObject, pos, 8f));
    }
}
