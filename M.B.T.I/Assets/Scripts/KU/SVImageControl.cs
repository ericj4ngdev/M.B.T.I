using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
public class SVImageControl : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    [SerializeField] private Image pickerImage;
    private RawImage SVImage;
    private ColorPickerControl CC;
    private RectTransform rectTransform;
    private RectTransform pickerTransform;

    public void OnDrag(PointerEventData eventData)
    {
        UpdateColor(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UpdateColor(eventData);
    }
    
    private void Awake()
    {
        SVImage = GetComponent<RawImage>();
        CC = FindObjectOfType<ColorPickerControl>();
        rectTransform = GetComponent<RectTransform>();

        pickerTransform = pickerImage.GetComponent<RectTransform>();
        pickerTransform.position =
            new Vector3(-(rectTransform.sizeDelta.x * 0.5f), -(rectTransform.sizeDelta.y * 0.5f));
    }

    void UpdateColor(PointerEventData eventData)
    {
        Vector3 pos = rectTransform.InverseTransformPoint(eventData.position);

        float deltaX = rectTransform.sizeDelta.x * 0.5f;
        float deltaY = rectTransform.sizeDelta.y * 0.5f;

        // pos.x = (pos.x < -deltaX) ? -deltaX : deltaX;
        // pos.y = (pos.y < -deltaY) ? -deltaY : deltaY;
        if (pos.x < -deltaX) pos.x = -deltaX;
        else if (pos.x > deltaX) pos.x = deltaX;
        if (pos.y < -deltaY) pos.y = -deltaY;
        else if (pos.y > deltaY) pos.y = deltaY;

        float x = pos.x + deltaX;
        float y = pos.y + deltaY;
        float xNorm = x / rectTransform.sizeDelta.x;
        float yNorm = y / rectTransform.sizeDelta.y;

        pickerTransform.localPosition = pos;
        pickerImage.color = Color.HSVToRGB(0, 0, 1 - yNorm);
        
        CC.SetSV(xNorm,yNorm);
    }
}
