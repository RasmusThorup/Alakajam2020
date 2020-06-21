//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBall : MonoBehaviour
{
    public InfectedSettings infectedSetting;
    public InfectedSettings citizenSetting;

    public Material citizenBodyMaterial;
    public Material citizenAreaMaterial;

    public Material infectedBodyMaterial;
    public Material infectedAreaMaterial;

    private MeshRenderer myBodyMaterial;
    private MeshRenderer myAreaMaterial;

    public GameObject triggerArea;
    public GameObject bodyObj;

    private WaveManager wavemanager;

    public float m_cachedLifeTime;
    public float m_cachedTriggerRadius;
    public float m_cachedVirusLevel;
    public float currentHealth;
    public float startHealth;
    public int m_cachedSpeed;

    public bool isScaling;

    public bool infected;
    public bool useLifeTime;
    public bool useHealth;
    public bool dead;
    public bool debug = false;
    public bool isMedic;
    public float animationSpeed = 0.4f;

    public AnimationCurve infectedColorCurve;

    private void Awake()
    {
        wavemanager = WaveManager.instance;
        myBodyMaterial = bodyObj.GetComponent<MeshRenderer>();
        myAreaMaterial = triggerArea.GetComponent<MeshRenderer>();
    }

    public virtual void OnEnable()
    {
        dead = false;
        wavemanager.activeBallList.Add(this);

        if (useHealth)
        {
            startHealth = Mathf.Clamp(citizenSetting.virusLevel+Random.Range(-100f,100f),10f,100f);
            currentHealth = startHealth;
        }
    }

    public virtual void OnDisable()
    {
        wavemanager.activeBallList.Remove(this);
        //Debug.Log("ball list contains: " + wavemanager.activeBallList.Count);
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

        if (useLifeTime)
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

        if (useHealth)
        {
            myBodyMaterial.material.Lerp(infectedBodyMaterial,citizenBodyMaterial,infectedColorCurve.Evaluate(Mathf.Clamp(currentHealth*0.01f,0f,1f)));

            /*
            if (infected)
            {
                if (currentHealth >= 1 && !isScaling)
                {
                    SetHealed();
                }
            }
            else
            {
                if (currentHealth <= -1 && !isScaling)
                {
                    SetInfected();
                }
            }
            */
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        
        if (useHealth || isScaling)
            return;

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

    protected virtual void OnTriggerStay(Collider other)
    {
        if (isScaling)
        {
            return;
        }

        if (infected)
        {
            BaseBall otherBall = other.GetComponentInParent<BaseBall>();

            if (otherBall == null)
            {
                return;
            }
            else if(!otherBall.useHealth)
            {
                return;
            }

            float infectChance = m_cachedVirusLevel / (m_cachedVirusLevel + otherBall.m_cachedVirusLevel);
            float dmg = infectChance*2;


            if (!otherBall.infected)
            {
                otherBall.currentHealth -= dmg;
                
                if (otherBall.currentHealth < -1)
                {
                    otherBall.SetInfected();
                }

            }
        }else if (isMedic)
        {
            BaseBall otherBall = other.GetComponentInParent<BaseBall>();

            if (otherBall == null)
            {
                return;
            }
            else if(!otherBall.useHealth)
            {
                return;
            }

            float infectChance = m_cachedVirusLevel / (m_cachedVirusLevel + otherBall.m_cachedVirusLevel);
            float heal = infectChance*2;

            if (otherBall.infected)
            {

                    otherBall.currentHealth += heal;
                    
                    if (otherBall.currentHealth > 1)
                    {
                        otherBall.SetHealed();
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

        myBodyMaterial.material = infectedBodyMaterial;
        myAreaMaterial.material = infectedAreaMaterial;

        infected = true;
        useLifeTime = true;
        
        //StopAllCoroutines();
        StartCoroutine(ScaleUp());
    }

    public virtual void SetHealed()
    {


        m_cachedTriggerRadius = citizenSetting.triggerRadius;
        m_cachedLifeTime = citizenSetting.lifeTime;
        m_cachedVirusLevel = citizenSetting.virusLevel;
        m_cachedSpeed = citizenSetting.speed;
        GameManager.Instance.scoreManager.DecreaseScore(100);

        myBodyMaterial.material = citizenBodyMaterial;
        myAreaMaterial.material = citizenAreaMaterial;

        infected = false;
        useLifeTime = false;
        
        //StopAllCoroutines();
        StartCoroutine(ScaleToNormal());
    }

    protected virtual void OnDeath()
    {
        dead = true;
        //Debug.Log("I Died!");
    }

    public IEnumerator ScaleUp()
    {
        if (isScaling)
        {
            yield break;
        }
        /*
        if (triggerArea.transform.localScale.x >= m_cachedTriggerRadius)
        {
            yield break;
        }
    */
        Vector3 originalSize = triggerArea.transform.localScale;
        float elapsedTime = 0f;

        isScaling = true;

        yield return pTween.To(animationSpeed, 0, 1, t => {
            triggerArea.transform.localScale = Vector3.Lerp(originalSize, new Vector3(1,1,1) * m_cachedTriggerRadius, t);
        });

        /*
        while (elapsedTime < animationSpeed)
        {
            triggerArea.transform.localScale = Vector3.Lerp(originalSize, new Vector3(1,1,1) * m_cachedTriggerRadius,
                (elapsedTime / animationSpeed));
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }*/

        isScaling = false;

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
        infected = false;
        this.gameObject.SetActive(false);

    }

    public IEnumerator ScaleToNormal()
    {
        if (isScaling)
        {
            yield break;
        }

        Vector3 originalSize = triggerArea.transform.localScale;
        Vector3 targetSize = new Vector3(1,1,1);
        float elapsedTime = 0f;

        isScaling = true;

        yield return pTween.To(animationSpeed, 0, 1, t => {
            triggerArea.transform.localScale = Vector3.Lerp(originalSize, targetSize, t);
        });
        /*
        while (elapsedTime < animationSpeed)
        {
            triggerArea.transform.localScale = Vector3.Lerp(originalSize, targetSize,
                (elapsedTime / animationSpeed));
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }*/

        isScaling = false;

        triggerArea.transform.localScale = targetSize;
    }
}
