using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberScript : MonoBehaviour
{
    public TextMeshProUGUI number;
    private int numberValue;

    private OneToTenScript masterScript;
    public void SetNumber(int number, OneToTenScript masterScript)
    {
        this.masterScript = masterScript;

        numberValue = number;
        this.number.text = number.ToString();
    }

    public void Check()
    {
        if (masterScript.CheckCounter(numberValue))
        {
            GetComponent<Image>().color = Color.green;
        }
        else
        {
            GetComponent<Image>().color = Color.red;
        }
    }
}
