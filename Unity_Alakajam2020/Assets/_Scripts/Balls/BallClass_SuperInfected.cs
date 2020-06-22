using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallClass_SuperInfected : BaseBall
{
    public InfectedSettings classSetting;

    public override void OnEnable()
    {

        m_cachedTriggerRadius = classSetting.triggerRadius;
        m_cachedLifeTime = classSetting.lifeTime;
        m_cachedVirusLevel = classSetting.virusLevel;
        m_cachedSpeed = classSetting.speed;

        infected = true;
        useLifeTime = true;
        base.OnEnable();
        StartCoroutine(ScaleUp());

        startHealth = m_cachedLifeTime;
        currentHealth = m_cachedLifeTime;
        isSuperinfected = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        BaseBall otherBall = other.GetComponentInParent<BaseBall>();
        if (otherBall.useHealth && !otherBall.infected)
        {
            otherBall.currentHealth = -1;
            otherBall.SetInfected();
            /*
            if (otherBall.objectAffectingBall)
            {
                otherBall.objectAffectingBall = null;
            }*/
            return;
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        return;
    }

    protected override void OnTriggerExit(Collider other)
    {
        return;
    }

    public override void SetInfected()
    {
        if (infected)
        {
            return;
        }

        //m_cachedTriggerRadius = classSetting.triggerRadius;
        //m_cachedLifeTime = classSetting.lifeTime;
        //m_cachedVirusLevel = classSetting.virusLevel;
        //m_cachedSpeed = classSetting.speed;

        base.SetInfected();
        m_cachedSpeed = 0;

    }

    protected override void OnDeath()
    {
        base.OnDeath();
        StartCoroutine(DeathAnimation());
    }
}
