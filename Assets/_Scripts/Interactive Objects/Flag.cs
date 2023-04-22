using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        Invoke(nameof(FinishGame), 2f);
    }

    private void FinishGame()
    {
        MySceneManager.Instance.GetNextScene();
    }
}
