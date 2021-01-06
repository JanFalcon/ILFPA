using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Highlight(bool highlight);
    void Interact();
    void Close();
}
