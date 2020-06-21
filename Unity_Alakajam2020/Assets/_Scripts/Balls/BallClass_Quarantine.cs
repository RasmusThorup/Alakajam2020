using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallClass_Quarantine : BaseBall
{
    public InfectedSettings classSetting;

    public override void OnEnable()
    {

        m_cachedTriggerRadius = citizenSetting.triggerRadius;
        m_cachedLifeTime = citizenSetting.lifeTime;
        m_cachedVirusLevel = citizenSetting.virusLevel;
        m_cachedSpeed = citizenSetting.speed;

        //infected = true;
        base.OnEnable();
        //StartCoroutine(ScaleUp());
    }

    public override void SetInfected()
    {
        if (infected)
        {
            return;
        }

        m_cachedTriggerRadius = classSetting.triggerRadius;
        m_cachedLifeTime = classSetting.lifeTime;
        m_cachedVirusLevel = classSetting.virusLevel;
        m_cachedSpeed = classSetting.speed;

        base.SetInfected();
        m_cachedSpeed = 0;

    }

    protected override void OnDeath()
    {
        base.OnDeath();
        StartCoroutine(DeathAnimation());
    }
}
