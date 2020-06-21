using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class Tooltip : MonoBehaviour
{
    TextMeshProUGUI tooltipText;
    RectTransform tooltipRect;
    //Transform tooltipTrans;
    Camera cam;
    Vector2 point;
    [ReadOnly]
    public bool tooltipActive;

    //-- Ugly singleton
    public static Tooltip Instance;
    //--

    void Awake()
    {
        Instance = this;
        cam = Camera.main;
        tooltipRect = transform.parent.GetComponent<RectTransform>();
        tooltipText = transform.GetComponentInChildren<TextMeshProUGUI>();
        HideTooltip();
    }

    void Update()
    {
        if (!tooltipActive)
            return;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(tooltipRect, Input.mousePosition, cam, out point);
        transform.localPosition = point;
    }

    void ShowTooltip(string tooltipString)
    {
        tooltipActive = true;
        gameObject.SetActive(tooltipActive);
        tooltipText.text = tooltipString;
        //float textPadding = 4f;
        //Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth+textPadding*2f, tooltipText.preferredHeight+textPadding*2f);
        //backgroundRectTrans.sizeDelta = backgroundSize;
    }

    void HideTooltip ()
    {
        tooltipActive = false;
        gameObject.SetActive(tooltipActive);
    }

    [Button("TestTooltip")]
    void Test()
    {
        ShowTooltip("Test String");
    }

}
