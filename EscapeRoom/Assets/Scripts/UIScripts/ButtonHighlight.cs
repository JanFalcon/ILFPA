using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool highLight = true;
    public float fontSizeAdder = 10f;

    private TextMeshProUGUI text;
    private string textBody;

    public TextMeshProUGUI descriptionText;
    public string description;

    private float fontSize;

    private void Awake()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        textBody = text.text;

        fontSize = text.fontSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (highLight)
        {
            text.text = $"< {textBody} >";
            text.fontSize = (fontSize + fontSizeAdder);
        }

        if (descriptionText)
        {
            descriptionText.text = description;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (highLight)
        {
            text.text = textBody;
            text.fontSize = fontSize;
        }
            
        if (descriptionText)
        {
            descriptionText.text = "";
        }
    }
}
