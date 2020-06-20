using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarantineBall : BaseBall
{
    public InfectedSettings classSetting;

    public override void OnEnable()
    {
        m_cachedTriggerRadius = classSetting.triggerRadius;
        m_cachedLifeTime = classSetting.lifeTime;
        m_cachedVirusLevel = classSetting.virusLevel;
        m_cachedSpeed = classSetting.speed;

        infected = true;
        base.OnEnable();
        StartCoroutine(ScaleUp());
    }

    public override void SetInfected()
    {
        if (infected)
        {
            return;
        }

        base.SetInfected();
        m_cachedSpeed = 0;

    }

    protected override void OnDeath()
    {
        base.OnDeath();
        StartCoroutine(DeathAnimation());
    }
}
