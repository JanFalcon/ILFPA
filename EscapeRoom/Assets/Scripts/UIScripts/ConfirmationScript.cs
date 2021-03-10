using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConfirmationScript : MonoBehaviour
{
    public Func<bool> MethodOverriding;

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
        RunTheMethod(MethodOverriding);
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
