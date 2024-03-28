using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;
using TMPro;

public class ObjSpawner : BaseCtrl
{
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private GameObject[] points;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject bossSpawnPoint;
    [SerializeField] private GameObject healthBarBoss;
    [SerializeField] private GameObject meteorite;
    [SerializeField] private GameObject[] items;
    [SerializeField] private GameObject Checkpoint;
    public int _turn;

    void Start()
    {
        StartCoroutine(Spawn(_turn));
    }

    IEnumerator SpawnEnemy()
    {
        List<GameObject> pickedPoints = new List<GameObject>();
        pickedPoints = points.ToList();
        while (pickedPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, pickedPoints.Count);
            GameObject spawnPoint = pickedPoints[randomIndex];
            pickedPoints.RemoveAt(randomIndex);
            Instantiate(enemys[Random.Range(0, enemys.Length)], spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Spawn(int turn)
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnItem());
        while (turn < 4)
        {
            yield return StartCoroutine(SpawnEnemy());
            yield return new WaitForSeconds(11f);
            yield return StartCoroutine(SpawnMeteorite());
            yield return new WaitForSeconds(5f);
            SpawnTurnEnemy();
            yield return new WaitForSeconds(13f);
            turn++;
            Checkpoint.SetActive(true);
            MapCtrl.Instance.CheckPoint();
            yield return new WaitForSeconds(3f);
            Checkpoint.SetActive(false);
        }
        SpawnBoss();
    }

    void SpawnTurnEnemy()
    {
        foreach (var spawnPoint in points)
            Instantiate(enemys[Random.Range(0, enemys.Length)], spawnPoint.transform.position, Quaternion.identity);
    }

    void SpawnBoss()
    {
        healthBarBoss.SetActive(true);
        Instantiate(boss, bossSpawnPoint.transform.position, Quaternion.identity);
    }

    IEnumerator SpawnMeteorite()
    {
        Vector2 minScene = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxScene = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        for (int i = 0; i <= 20; i ++)
        {
            Instantiate(meteorite, new Vector2(Random.Range(minScene.x, maxScene.x), maxScene.y), Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator SpawnItem()
    {
        Vector2 minScene = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxScene = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        for (int i = 0; i <= Random.Range(5, 6); i++)
        {
            yield return new WaitForSeconds(Random.Range(15, 20));
            Instantiate(items[Random.Range(0, items.Length)], new Vector2(Random.Range(minScene.x, maxScene.x), maxScene.y), Quaternion.identity);
        }
    }
}
