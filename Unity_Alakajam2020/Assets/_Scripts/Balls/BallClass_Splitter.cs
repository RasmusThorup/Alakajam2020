using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallClass_Splitter : BaseBall
{
    public InfectedSettings classSetting;
    public GameObject prefab;
    public int spawnAmount = 2;

    public override void OnEnable()
    {
        m_cachedTriggerRadius = classSetting.triggerRadius;
        m_cachedLifeTime = classSetting.lifeTime;
        m_cachedVirusLevel = classSetting.virusLevel;
        m_cachedSpeed = classSetting.speed;

        useLifeTime = true;
        infected = true;
        base.OnEnable();
    }

    
    protected override void OnDeath()
    {
        base.OnDeath();

        StartCoroutine(DeathAnimation());

        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(prefab, transform.position, Quaternion.identity); 
        }
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
