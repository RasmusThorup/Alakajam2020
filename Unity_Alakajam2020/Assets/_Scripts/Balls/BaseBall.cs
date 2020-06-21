//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBall : MonoBehaviour
{
    public InfectedSettings infectedSetting;
    public InfectedSettings citizenSetting;

    public float m_cachedLifeTime;
    public float m_cachedTriggerRadius;
    public float m_cachedVirusLevel;
    public int m_cachedSpeed;
    public GameObject triggerArea;
    public bool infected;
    public bool dead;
    public bool debug = false;
    public bool isMedic;
    public float animationSpeed = 0.2f;


    public virtual void OnEnable()
    {
        dead = false;
    }

    public virtual void Update()
    {
        if (debug)
        {
            if (Input.anyKey)
            {
                SetInfected();
            }
        }
       
        if (infected)
        {
            if (m_cachedLifeTime <= 0)
            {
                if (!dead)
                {
                    OnDeath();
                }
            }
            m_cachedLifeTime -= Time.deltaTime;
        } 
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (infected)
        {
            BaseBall otherBall = other.GetComponentInParent<BaseBall>();

            if (otherBall == null || otherBall.isMedic)
            {
                return; 
            }
            
            if (!otherBall.infected)
            {
                float infectChance = m_cachedVirusLevel / (m_cachedVirusLevel + otherBall.m_cachedVirusLevel);
                if (Random.value < infectChance)
                {
                    otherBall.SetInfected();
                }
            }
        }
    }

    public virtual void SetInfected()
    {
        m_cachedTriggerRadius = infectedSetting.triggerRadius;
        m_cachedLifeTime = infectedSetting.lifeTime;
        m_cachedVirusLevel = infectedSetting.virusLevel;
        m_cachedSpeed = infectedSetting.speed;
        GameManager.Instance.scoreManager.IncreaseScore(100);
        infected = true;
        StartCoroutine(ScaleUp());
    }

    public virtual void SetHealed()
    {
        m_cachedTriggerRadius = citizenSetting.triggerRadius;
        m_cachedLifeTime = citizenSetting.lifeTime;
        m_cachedVirusLevel = citizenSetting.virusLevel;
        m_cachedSpeed = citizenSetting.speed;
        GameManager.Instance.scoreManager.DecreaseScore(100);
        StartCoroutine(ScaleToNormal());
    }

    protected virtual void OnDeath()
    {
        dead = true;
       // Debug.Log("I Died!");
    }

    public IEnumerator ScaleUp()
    {
        if (triggerArea.transform.localScale.x >= m_cachedTriggerRadius)
        {
            yield return null;
        }
        Vector3 originalSize = triggerArea.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < animationSpeed)
        {
            triggerArea.transform.localScale = Vector3.Lerp(originalSize, originalSize * m_cachedTriggerRadius,
                (elapsedTime / animationSpeed));
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        triggerArea.transform.localScale = originalSize * m_cachedTriggerRadius;
    }

    public IEnumerator DeathAnimation()
    {
        Vector3 originalSize = this.transform.localScale;
        Vector3 targetSize = Vector3.zero;
        float elapsedTime = 0f;

        while (elapsedTime < animationSpeed)
        {
            this.transform.localScale = Vector3.Lerp(originalSize, targetSize,
                (elapsedTime / animationSpeed));
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        this.transform.localScale = targetSize;
        this.gameObject.SetActive(false);
    }

    public IEnumerator ScaleToNormal()
    {
        Vector3 originalSize = triggerArea.transform.localScale;
        Vector3 targetSize = new Vector3(1,1,1);
        float elapsedTime = 0f;

        while (elapsedTime < animationSpeed)
        {
            triggerArea.transform.localScale = Vector3.Lerp(originalSize, targetSize,
                (elapsedTime / animationSpeed));
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        this.transform.localScale = targetSize;
    }
}

