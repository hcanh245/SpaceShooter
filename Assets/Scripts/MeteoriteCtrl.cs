using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteCtrl : BaseCtrl
{
    private float _rotationSpeed = 500f;
    private const int _maxHealth = 3;
    private const int _score = 100;
    private int _curHealth;
    [SerializeField] private GameObject expAnim;

    void Start()
    {
        _curHealth = _maxHealth;
    }

    void Update()
    {
        float currentRotation = transform.rotation.eulerAngles.z;
        float newRotation = currentRotation + (_rotationSpeed * Time.deltaTime);
        if (newRotation > 360f)
        {
            newRotation -= 360f;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, newRotation);
        Vector2 pos = transform.position;
        pos += new Vector2 (0.05f, -1) * 2f * Time.deltaTime;
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            DestroyEnemy(_score, expAnim, gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        _curHealth -= damage;
        if (_curHealth <= 0)
        {
            DestroyEnemy(_score, expAnim, gameObject);
        }
    }
}
