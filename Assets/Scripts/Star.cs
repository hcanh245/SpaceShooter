using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] public float speed;
    void Start()
    {
        speed = 0.4f;
    }
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        Vector2 minScene = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxScene = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (transform.position.y < minScene.y)
            transform.position = new Vector2(Random.Range(minScene.x, maxScene.x), maxScene.y);
    }
}
