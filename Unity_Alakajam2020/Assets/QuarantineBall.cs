using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarantineBall : BaseBall
{
    public InfectedSettings classSetting;

    public override void OnEnable()
    {
        //m_cachedTriggerRadius = classSetting.triggerRadius;
        //m_cachedLifeTime = classSetting.lifeTime;
        //m_cachedVirusLevel = classSetting.virusLevel;
        //m_cachedSpeed = classSetting.speed;
    }

    public override void SetInfected()
    {
        if (infected)
        {
            return;
        }
        infected = classSetting;

        base.SetInfected();
        m_cachedSpeed = 0;
        //StartCoroutine(ScaleUp());

    }

    protected override void OnDeath()
    {
        base.OnDeath();
        StartCoroutine(DeathAnimation());
    }
}
