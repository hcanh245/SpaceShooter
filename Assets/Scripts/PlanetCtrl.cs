using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCtrl : MonoBehaviour
{
    [SerializeField] private GameObject[] Planets;
    Queue<GameObject> PlanetQueue = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject planet in Planets) { PlanetQueue.Enqueue(planet); }
        InvokeRepeating("Move", 0, 5f);
    }

    void Move()
    {
        EnqueuePlanet();
        if (PlanetQueue.Count == 0)
            return;
        GameObject planet = PlanetQueue.Dequeue();
        planet.GetComponent<Planet>().isMoving = true;
    }

    void EnqueuePlanet()
    {
        foreach(GameObject planet in Planets)
        {
            if((planet.transform.position.y < 0) && (!planet.GetComponent<Planet>().isMoving))
            {
                planet.GetComponent<Planet>().ResetPos();
                PlanetQueue.Enqueue(planet);
            }
        }
    }
}
