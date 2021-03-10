using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract instance;

    public GameObject interactUI;
    private GameObject _interactUI;

    public float detectionRadius = 1f;
    public Vector2 offset;
    private readonly int bitMask = 1 << 9;  //Interactable Layer
    public bool InArea { get; set; } = false;

    private Transform canvas;
    private PlayerMovementScript playerMovement;

    public IInteractable interact;
    private bool interactable = false;
    private bool interacting = false;
    private Collider2D[] col;
    private Transform target;
    private void Awake()
    {
        instance = this;
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    public void InstantiateUI()
    {
        if (_interactUI != null)
        {
            return;
        }
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
        col = Physics2D.OverlapCircleAll((Vector2)transform.position + offset, detectionRadius, bitMask);

        if (col.Length > 0)
        {
            //Get Nearest Collider
            foreach (Collider2D c in col)
            {

                if (c.transform.position.y < transform.position.y)
                {
                    continue;
                }

                if (target != null)
                {
                    Vector2 distance1 = c.transform.position - transform.position;
                    Vector2 distance2 = target.position - transform.position;

                    interactable = true;

                    if (distance1.sqrMagnitude < distance2.sqrMagnitude)
                    {
                        interact?.Highlight(false);
                        target = c.transform;
                        interact = target.GetComponent<IInteractable>();
                        interact?.Highlight(true);
                    }
                }
                else
                {
                    interact?.Highlight(false);
                    target = c.transform;
                    interact = target.GetComponent<IInteractable>();
                    interact?.Highlight(true);
                }
            }

            if (!InArea && interactable)
            {
                InstantiateUI();
                InArea = true;
            }


            if (Input.GetKeyDown(KeyCode.E) && interact != null)
            {
                if (!interacting)
                {
                    GameManager.instance.Interact();
                    playerMovement.enabled = false;
                    interacting = true;
                    interact.Interact();
                    DestroyInteract();
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
        target = null;
        InArea = false;
        interactable = false;
        interacting = false;
        DestroyInteract();

        if (interact != null)
        {
            interact.Highlight(false);
            interact = null;
        }

        playerMovement.enabled = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere((Vector2)transform.position + offset, detectionRadius);
    }
}
