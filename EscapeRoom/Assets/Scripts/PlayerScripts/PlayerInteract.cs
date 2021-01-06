using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject interactUI;

    public float detectionRadius = 1f;
    public float updatePerSecond = 10f;

    private Vector2 talkingTo;

    private readonly int bitMask = 1 << 9;  //Interactable Layer
    public bool InArea { get; set; } = false;

    private IInteractable interact;
    private bool interacting = false;

    private void Update()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, detectionRadius, bitMask);

        if (col)
        {
            //Get Interface
            if (!InArea)
            {
                interact = col.GetComponent<IInteractable>();
            }

            //Highlight gameobject
            if (interact != null && !InArea)
            {
                interact.Highlight(true);
            }

            InArea = true;                                                          //can be removed
            talkingTo = (Vector2)col.transform.position;                            //can be removed

            interactUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (interact != null)
                {
                    if (!interacting)
                    {
                        interact.Interact();
                        interacting = true;
                    }
                    else
                    {
                        interact.Close();
                        interacting = false;
                    }
                }
            }
        }
        else
        {
            if (InArea)
            {
                InArea = false;
                interacting = false;
                interactUI.SetActive(false);

                if (interact != null)
                {
                    interact.Highlight(false);
                    interact.Close();
                    interact = null;
                }
            }
        }
    }

    public Vector2 GetTalkingTo()
    {
        return talkingTo;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
