using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject interactUI;
    private GameObject _interactUI;

    public float detectionRadius = 1f;

    private readonly int bitMask = 1 << 9;  //Interactable Layer
    public bool InArea { get; set; } = false;

    private PlayerMovementScript playerMovement;

    private IInteractable interact;
    private bool interacting = false;

    private void Awake()
    {
        _interactUI = GameObject.FindGameObjectWithTag("InteractUI");
        if (!_interactUI)
        {
            InstantiateUI();
        }
        _interactUI.SetActive(false);
    }

    public void InstantiateUI()
    {
        Transform canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        _interactUI = Instantiate(interactUI, canvas);
    }

    private void Start()
    {
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
                _interactUI.SetActive(true);
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
                        _interactUI.SetActive(false);
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
        _interactUI.SetActive(false);

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
