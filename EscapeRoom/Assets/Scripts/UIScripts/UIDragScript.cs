using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class UIDragScript : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler
{
    public static UIDragScript uIDragScript;
    private Canvas canvas;
    private RectTransform rectTransform;
    private Image image;
    public GameObject linePrefab;
    public string itemName;
    private GameObject line;
    private RectTransform lineRectTransform;

    private bool updateLine = true;
    private Color color;
    private Game1UIScript game1UIScript;
    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void SetValues(Vector2 pos, string itemName, Color color, Game1UIScript game1UIScript)
    {
        this.game1UIScript = game1UIScript;
        rectTransform.localPosition = pos;
        this.itemName = itemName;
        this.color = color;

        image.color = color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        line = Instantiate(linePrefab, transform.position, Quaternion.identity, transform.parent) as GameObject;
        line.GetComponent<Image>().color = color;
        lineRectTransform = line.GetComponent<RectTransform>();
        UpdateLine(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!this.Equals(uIDragScript) && itemName.Equals(uIDragScript.itemName))
        {
            updateLine = false;
            Connect(uIDragScript.transform.position);
            game1UIScript.Check();
            Destroy(uIDragScript);
            Destroy(this);
        }
        else
        {
            Destroy(line);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateLine(eventData.position);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uIDragScript = this;
    }

    private void UpdateLine(Vector3 pos)
    {
        if (!updateLine)
        {
            return;
        }

        //Update Direction
        Vector3 dir = (pos - transform.position);
        line.transform.right = dir;

        //Update Scale
        // line.transform.localScale = new Vector3(dir.magnitude * canvas.scaleFactor, 1f, 1f);
        lineRectTransform.sizeDelta = new Vector2(dir.magnitude / canvas.scaleFactor, 10f);
    }

    public void Connect(Vector3 pos)
    {
        //Update Direction
        Vector3 dir = (pos - transform.position);
        line.transform.right = dir;

        //Update Scale
        // line.transform.localScale = new Vector3(dir.magnitude * canvas.scaleFactor + 5f, 1f, 1f);
        lineRectTransform.sizeDelta = new Vector2(dir.magnitude / canvas.scaleFactor, 10f);
    }
}
