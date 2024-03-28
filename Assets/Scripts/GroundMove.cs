using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMove : MonoBehaviour
{
    void Update()
    {
        if (transform.localPosition.x < -1.45f)
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
        }
        transform.Translate(-Time.deltaTime, 0, 0);
    }
}
