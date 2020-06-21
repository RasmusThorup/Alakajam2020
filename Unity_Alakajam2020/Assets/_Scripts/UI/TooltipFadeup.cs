using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipFadeup : MonoBehaviour
{
    public float fadeTime = 1f;
    public AnimationCurve fadeCurve;
    CanvasGroup canvasGroup;

    float currentAlpha;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        currentAlpha = canvasGroup.alpha;
    } 

    public void ShowTooltip()
    {
        StopAllCoroutines();
        StartCoroutine(ShowTooltip_Routine());
    }

    public void HideTooltip()
    {
        StopAllCoroutines();
        StartCoroutine(HideTooltip_Routine());
    }

    IEnumerator ShowTooltip_Routine()
    {
        yield return pTween.To(fadeTime,currentAlpha, 1, t =>{
            canvasGroup.alpha = fadeCurve.Evaluate(t);
            currentAlpha = canvasGroup.alpha;
        });
    }
    IEnumerator HideTooltip_Routine()
    {
        yield return pTween.To(fadeTime,currentAlpha, 0, t =>{
            canvasGroup.alpha = fadeCurve.Evaluate(t);
            currentAlpha = canvasGroup.alpha;
        });
    }
}
