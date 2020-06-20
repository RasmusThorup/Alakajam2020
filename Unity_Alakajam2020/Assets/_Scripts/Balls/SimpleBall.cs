using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBall : BaseBall
{
    public override void OnDeath()
    {
        Debug.Log("I Died!");
        base.OnDeath();
    }

    public override void OnInfected()
    {
        Debug.Log("Im Infected!");
        base.OnInfected();
    }
    
}
