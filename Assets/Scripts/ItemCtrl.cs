using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : MonoBehaviour
{
    void Update()
    {
        Vector2 pos = transform.position;
        pos += Vector2.down * Time.deltaTime;
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            Destroy(gameObject);
    }
}
