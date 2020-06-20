using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallClass_Medic : BaseBall
{
    public InfectedSettings classSetting;

    public override void OnEnable()
    {
        m_cachedTriggerRadius = classSetting.triggerRadius;
        m_cachedLifeTime = classSetting.lifeTime;
        m_cachedVirusLevel = classSetting.virusLevel;
        m_cachedSpeed = classSetting.speed;

        base.OnEnable();
        isMedic = true;
        StartCoroutine(ScaleUp());
    }

    protected override void OnTriggerEnter(Collider other)
    {
        BaseBall otherBall = other.GetComponentInParent<BaseBall>();

        if (otherBall.infected)
        {
            float healChance = m_cachedVirusLevel / (m_cachedVirusLevel + otherBall.m_cachedVirusLevel);
            
            if (Random.value < healChance)
            {
                otherBall.infected = false;
                otherBall.SetHealed();
            }
            else
            {
                OnDeath();
            }
        }
    }


    public override void SetInfected()
    {
        if (infected)
        {
            return;
        }
        //infected = overrideSettings;

        StartCoroutine(ScaleToNormal());
        base.SetInfected();
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        StartCoroutine(DeathAnimation());
    }
}
