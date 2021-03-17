using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ConfirmationScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Func<bool> MethodOverriding;

    public void SetUp(string text)
    {
        this.text.text = text;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Confirm();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }
    }

    public void Confirm()
    {
        if (MethodOverriding != null)
        {
            RunTheMethod(MethodOverriding);
        }
        else
        {
            Cancel();
        }
    }

    public void RunTheMethod(Func<bool> Function)
    {
        if (Function())
        {
            Cancel();
        }
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
}
