using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallClass_Medic : BaseBall
{
    public InfectedSettings overrideSettings;

    public override void OnEnable()
    {
        base.OnEnable();
        isMedic = true;
        StartCoroutine(ScaleUp());
    }

    protected override void OnTriggerEnter(Collider other)
    {
        BaseBall otherBall = other.GetComponentInParent<BaseBall>();

        if (otherBall.infected)
        {
            int healChance = m_cachedVirusLevel / (m_cachedVirusLevel + otherBall.m_cachedVirusLevel);
            if (Random.value < healChance)
            {
                otherBall.infected = false;
                otherBall.StartCoroutine(ScaleDown());
            }
            else
            {
                SetInfected();
            }
        }
    }


    public override void SetInfected()
    {
        if (infected)
        {
            return;
        }
        infectedSetup = overrideSettings;


        base.SetInfected();
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        StartCoroutine(ScaleDown());
    }
}
