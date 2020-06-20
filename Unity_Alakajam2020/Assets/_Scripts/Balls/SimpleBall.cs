using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBall : BaseBall
{
    protected override void OnDeath()
    {
        base.OnDeath();

        StartCoroutine(ScaleDown()); 
    }

    public override void SetInfected()
    {
        
        if (infected)
        {
            return;
        }
        base.SetInfected();
        StartCoroutine(ScaleUp()); 
        //triggerArea.transform.localScale = transform.localScale * m_cachedTriggerRadius; 
    }
}
