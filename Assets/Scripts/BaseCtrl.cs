using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class BaseCtrl : MonoBehaviour
{
    public IEnumerator MoveBetweenPositions(Vector2 startPosition, Vector2 endPosition, float duration)
    {
        float timeElapsed = 0f;
        float halfDuration = duration / 2f;

        while (timeElapsed < duration)
        {
            float t = Mathf.PingPong(timeElapsed, halfDuration) / halfDuration;
            transform.position = Vector2.Lerp(startPosition, endPosition, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = startPosition;
    }

    public IEnumerator MoveToPosition(GameObject obj, Vector2 targetPosition, float duration)
    {
        Vector2 startPosition = obj.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            obj.transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPosition;
    }

    public void DestroyEnemy(int score, GameObject expAnim, GameObject obj)
    {
        MapCtrl.Instance.SetScore(score);
        Instantiate(expAnim, obj.transform.position, Quaternion.identity);
        Destroy(obj);
    }

    public IEnumerator BlinkText(TextMeshProUGUI textToBlink, float blinkSpeed)
    {
        while (true)
        {
            textToBlink.color = new Color(textToBlink.color.r, textToBlink.color.g, textToBlink.color.b, 0);
            yield return new WaitForSeconds(blinkSpeed);
            textToBlink.color = new Color(textToBlink.color.r, textToBlink.color.g, textToBlink.color.b, 0.5f);
            yield return new WaitForSeconds(blinkSpeed);
        }
    }

    public void SaveData(GameData data)
    {
        string file = "save.json";
        string filePath = Path.Combine(Application.persistentDataPath, file);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }

    public GameData LoadData()
    {
        string file = "save.json";
        string filePath = Path.Combine(Application.persistentDataPath, file);
        if (!File.Exists(filePath))
            File.WriteAllText(filePath, "");
        GameData data = JsonUtility.FromJson<GameData>(File.ReadAllText(filePath));
        return data;
    }
}
