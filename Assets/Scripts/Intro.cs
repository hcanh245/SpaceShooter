using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : BaseCtrl
{
    public GameObject playerIntro;
    public GameObject expIntroAnim;
    public IEnumerator IntroGame()
    {
        yield return StartCoroutine(MoveToPosition(gameObject, new Vector2(1, -3), 3f));
        yield return new WaitForSeconds(0.5f);
        Instantiate(expIntroAnim, playerIntro.transform.position, Quaternion.identity);
        playerIntro.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(MoveToPosition(gameObject, new Vector2(1, 7), 3f));
    }
}
