using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvaluationScript : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void SetEvaluation(string eval)
    {
        text.text = eval;
    }

    public void NextQuestion()
    {
        GameManager.instance.ResetTime();
    }
}
