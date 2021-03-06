﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallClass_Citizen : BaseBall
{
    public InfectedSettings classSetting;

    public override void OnEnable()
    {
        m_cachedTriggerRadius = classSetting.triggerRadius;
        m_cachedLifeTime = classSetting.lifeTime;
        m_cachedVirusLevel = classSetting.virusLevel;
        m_cachedSpeed = classSetting.speed;

        useLifeTime = false;
        base.OnEnable();
    }

    protected override void OnDeath()
    {
        base.OnDeath();

        StartCoroutine(DeathAnimation());
    }

    public override void SetInfected()
    {

        if (infected)
        {
            return;
        }

        infected = infectedSetting;

        base.SetInfected();

        //triggerArea.transform.localScale = transform.localScale * m_cachedTriggerRadius; 
    }
}
