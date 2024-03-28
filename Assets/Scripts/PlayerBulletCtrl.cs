using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    private float _speed;
    private int _dame;
    [SerializeField] private BulletInfo bulletInfo;

    void Start()
    {
        init(bulletInfo);
    }

    void init(BulletInfo info)
    {
        _speed = info.speed;
        _dame = info.dame;
    }

    void Update()
    {
        Vector2 pos = transform.position;
        pos = new Vector2(pos.x, pos.y + _speed * Time.deltaTime);
        transform.position = pos;
        Vector2 maxScene = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (transform.position.y > maxScene.y)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyCtrl enemy = col.GetComponent<EnemyCtrl>();
            if (enemy != null)
            {
                enemy.TakeDamage(_dame);
            }
            Destroy(gameObject);
        }
        if (col.CompareTag("Meteorite"))
        {
            MeteoriteCtrl meteorite = col.GetComponent<MeteoriteCtrl>();
            if (meteorite != null)
            {
                meteorite.TakeDamage(_dame);
            }
            Destroy(gameObject);
        }
        if (col.CompareTag("Boss"))
        {
            BossCtrl boss = col.GetComponent<BossCtrl>();
            if (boss != null)
            {
                boss.TakeDamage(_dame);
            }
            Destroy(gameObject);
        }
    }
}
