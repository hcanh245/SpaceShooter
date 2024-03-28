using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    float speed;
    public bool isMoving;
    Vector2 minScene;
    Vector2 maxScene;
    void Awake()
    {
        speed = 0.6f;
        isMoving = false;
        minScene = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxScene = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        minScene.y -= 5f;
        maxScene.y += 2.5f;
    }
    void Update()
    {
        if (!isMoving)
            return;
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y < minScene.y)
            isMoving = false;
    }

    public void ResetPos()
    {
        transform.position = new Vector2(Random.Range(minScene.x, maxScene.x), maxScene.y);
    }
}
