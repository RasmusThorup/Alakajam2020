using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarantineBall : BaseBall
{
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
}
