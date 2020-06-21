using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WwiseButton : MonoBehaviour
{
    public AK.Wwise.Event Hover;
    public AK.Wwise.Event Click; 
    
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Hover.Post(gameObject); 
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Click.Post(gameObject);
    }


}
