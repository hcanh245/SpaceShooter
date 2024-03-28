using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : BaseCtrl
{
    public TextMeshProUGUI storyText;
    public TextMeshProUGUI textToBlink;
    float letterDelay = 0.05f;
    string message;
    Coroutine myCoroutine;

    void Start()
    {
        message = storyText.text;
        myCoroutine = StartCoroutine(ShowText());
        StartCoroutine(BlinkText(textToBlink, 0.4f));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine(myCoroutine);
            storyText.text = message;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= message.Length; i++)
        {
            storyText.text = message.Substring(0, i);
            yield return new WaitForSeconds(letterDelay);
        }
    }
}
