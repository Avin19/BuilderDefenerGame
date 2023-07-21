using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipUI : MonoBehaviour
{
    public static ToolTipUI Instance { get; private set; }
    private TextMeshProUGUI textMeshPro;
    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;

    [SerializeField] private RectTransform canvasRectTransfomr;

    private void Awake()
    {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();  
        textMeshPro= transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();

        Hide();
    }
    private void SetText(string text)
    {
        textMeshPro.text = text;
        textMeshPro.ForceMeshUpdate();


        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8f, 8f);
        backgroundRectTransform.sizeDelta = textSize+padding;

    }
    private void Update()
    {
        Vector2 anchoredPosition = Input.mousePosition /canvasRectTransfomr.localScale.x;

        if(anchoredPosition.x +backgroundRectTransform.rect.width > canvasRectTransfomr.rect.width)

        {
            anchoredPosition.x = canvasRectTransfomr.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransfomr.rect.height)

        {
            anchoredPosition.y = canvasRectTransfomr.rect.height - backgroundRectTransform.rect.height;
        }
        rectTransform.anchoredPosition = anchoredPosition;
    }
    public void Show(string toolTipText)
    {
        gameObject.SetActive(true);
        SetText(toolTipText);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
