using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawn : MonoBehaviour
{
    [SerializeField] private GameObject Star;
    private int MaxStars;
    void Start()
    {
        MaxStars = 40;
        Vector2 minScene = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxScene = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        for (int i = 0; i < MaxStars; i++)
        {
            GameObject star = Instantiate(Star, new Vector2(Random.Range(minScene.x, maxScene.x), Random.Range(minScene.y, maxScene.y)), Quaternion.identity);
            star.GetComponent<Star>().speed = (1f * Random.value + 0.5f);
            star.transform.parent = transform;
        }
    }
}
