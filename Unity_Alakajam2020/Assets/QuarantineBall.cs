﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarantineBall : BaseBall
{
    public float animationSpeed = 0.2f;
    public override void SetInfected()
    {
        if (infected)
        {
            return;
        }
        base.SetInfected();
        m_cachedSpeed = 0;
        StartCoroutine(ScaleUp());

    }

    protected override void OnDeath()
    {
        base.OnDeath();
        StartCoroutine(ScaleDown());
    }
    public IEnumerator ScaleUp()
    {
        Vector3 originalSize = triggerArea.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < animationSpeed)
        {
            triggerArea.transform.localScale = Vector3.Lerp(originalSize, originalSize * m_cachedTriggerRadius,
                (elapsedTime / animationSpeed));
            elapsedTime += Time.deltaTime; 
            
            yield return new WaitForEndOfFrame();
        }

        triggerArea.transform.localScale = originalSize * m_cachedTriggerRadius; 

    }

    public IEnumerator ScaleDown()
    {
        Vector3 originalSize = this.transform.localScale;
        Vector3 targetSize = Vector3.zero; 
        float elapsedTime = 0f;

        while (elapsedTime < animationSpeed)
        {
            this.transform.localScale = Vector3.Lerp(originalSize, targetSize,
                (elapsedTime / animationSpeed));
            elapsedTime += Time.deltaTime; 
            
            yield return new WaitForEndOfFrame();
        }

        this.transform.localScale = targetSize; 
        this.gameObject.SetActive(false);
        
    }
    
    
    
    
    
}