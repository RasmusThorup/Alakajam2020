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

    protected override void OnInfected()
    {
        base.OnInfected();
        triggerArea.transform.localScale = transform.localScale * triggerRadius; 
    }
    
}
