using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuideManager : BaseCtrl
{
    public TextMeshProUGUI textToBlink;
    void Start()
    {
        StartCoroutine(BlinkText(textToBlink, 0.4f));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
