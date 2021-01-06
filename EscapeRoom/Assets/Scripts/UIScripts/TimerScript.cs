using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    private GameManager gameManager;
    private TextMeshProUGUI timerText;
    private float timer;

    private void Start()
    {
        gameManager = GameManager.instance;

        timerText = GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {
        timer = gameManager.GetTimer();
        timerText.SetText(string.Format("Timer : {0:0.0}", timer));
    }
}
