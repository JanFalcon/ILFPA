﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract instance;

    public GameObject interactUI;
    private GameObject _interactUI;

    public float detectionRadius = 1f;

    private readonly int bitMask = 1 << 9;  //Interactable Layer
    public bool InArea { get; set; } = false;

    private Transform canvas;
    private PlayerMovementScript playerMovement;

    private IInteractable interact;
    private bool interacting = false;

    private void Awake()
    {
        instance = this;
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    public void InstantiateUI()
    {
        _interactUI = Instantiate(interactUI, canvas);
        _interactUI.transform.SetAsFirstSibling();
    }

    public void DestroyInteract()
    {
        if (_interactUI)
        {
            Destroy(_interactUI);
        }
    }

    private void Start()
    {
        AudioManager.instance.StopTheme();
        playerMovement = GetComponent<PlayerMovementScript>();
    }

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
            if (!InArea && interact != null)
            {
                InstantiateUI();
                interact.Highlight(true);
            }
            InArea = true;                                                          

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (interact != null)
                {
                    if (!interacting)
                    {
                        playerMovement.enabled = false;
                        interact.Interact();
                        interacting = true;
                        DestroyInteract();
                    }
                }
            }
        }
        else
        {
            if (InArea)
            {
                Close();
            }
        }
    }

    public void Close()
    {
        InArea = false;
        interacting = false;
        DestroyInteract();

        if (interact != null)
        {
            interact.Highlight(false);
            interact.Close();
            interact = null;
        }

        playerMovement.enabled = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
