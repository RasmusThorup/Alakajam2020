using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TooltipFadeup tooltipFade;

    void Awake()
    {
        tooltipFade = GetComponentInChildren<TooltipFadeup>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipFade.ShowTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipFade.HideTooltip();
    }
}
