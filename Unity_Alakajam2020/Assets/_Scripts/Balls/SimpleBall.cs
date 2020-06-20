using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBall : BaseBall
{
    protected override void OnDeath()
    {
        base.OnDeath();
        
        this.gameObject.SetActive(false);
    }

    protected override void SetInfected()
    {
        base.SetInfected();

        StartCoroutine(ScaleUp()); 
        //triggerArea.transform.localScale = transform.localScale * m_cachedTriggerRadius; 
    }


    public IEnumerator ScaleUp()
    {
        Vector3 originalSize = triggerArea.transform.localScale;

        float animationSpeed = 0.5f;
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
    
}
