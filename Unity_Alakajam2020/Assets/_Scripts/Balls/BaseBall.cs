//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BaseBall : MonoBehaviour
{
    public InfectedSettings infectedSetting;
    public InfectedSettings citizenSetting;
    [BoxGroup("Materials")]
    public Material citizenBodyMaterial;
    [BoxGroup("Materials")]
    public Material citizenAreaMaterial;
    [BoxGroup("Materials")]
    public Material infectedBodyMaterial;
    [BoxGroup("Materials")]
    public Material infectedAreaMaterial;
    [BoxGroup("Materials")]
    public Material medicBodyMaterialHealthy;
    [BoxGroup("Materials")]
    public Material medicBodyMaterialDie;

    private MeshRenderer myBodyMaterial;
    private MeshRenderer myAreaMaterial;

    public GameObject triggerArea;
    public GameObject bodyObj;

    private WaveManager wavemanager;

    public float m_cachedLifeTime;
    public float m_cachedTriggerRadius;
    public float m_cachedVirusLevel;
    [ReadOnly]
    public float currentHealth = 0;
    public float startHealth;
    public int m_cachedSpeed;
    [ReadOnly]
    public bool isScaling;
    [ReadOnly]
    public bool isGettingInfected;
    public GameObject objectAffectingBall;

    public bool infected;
    public bool useLifeTime;
    public bool useHealth;
    public bool dead;
    public bool debug = false;
    public bool isMedic;
    public float animationSpeed = 0.4f;

    public AnimationCurve infectedColorCurve;
    [HideInInspector]
    public bool isSuperinfected;

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
            //TODO: currently we are using citizensetting.virusLevel. Maybe we should be using m_cachedVirusLevel, because it will always be according to the BallClass.
            //TODO: The problem is that OnEnable only happens once. And we want this to be set everytime we change BallType. Like going from QuarantineBall to CitizenBall to InfectedBall to CitizenBall to InfectedBall.
            //startHealth = citizenSetting.virusLevel;
            startHealth = m_cachedVirusLevel;
            currentHealth = Random.Range(m_cachedVirusLevel * 0.1f, m_cachedVirusLevel);
        }
    }

    public virtual void OnDisable()
    {
        wavemanager.activeBallList.Remove(this);
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

        if (isMedic)
        {
            myBodyMaterial.material.Lerp(medicBodyMaterialDie, medicBodyMaterialHealthy, Mathf.Clamp(currentHealth/startHealth, 0f, 1f)); 
        }
        if (useHealth)
        {
            if (infected)
            {
                myBodyMaterial.material.Lerp(infectedBodyMaterial, citizenBodyMaterial, infectedColorCurve.Evaluate(Mathf.Clamp(currentHealth/startHealth*0.5f, 0f, 1f)));
            }
            else
            {
                myBodyMaterial.material.Lerp(infectedBodyMaterial, citizenBodyMaterial, infectedColorCurve.Evaluate(Mathf.Clamp(currentHealth/startHealth, 0f, 1f)));
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (isScaling)
            return;
        
        BaseBall otherBall = other.GetComponentInParent<BaseBall>();

        if (infected)
        {
            if (otherBall == null || otherBall.isMedic)
            {
                return;
            }

            if (otherBall.useHealth && !otherBall.infected)
            {
                //otherBall.objectAffectingBall = gameObject;
                return;   
            }

            if (!otherBall.infected)
            {
                float infectChance = m_cachedVirusLevel / (m_cachedVirusLevel + otherBall.m_cachedVirusLevel);
                if (Random.value < infectChance)
                {
                    Debug.Log("set Infected");
                    otherBall.SetInfected();
                }
            }
        }else if (isMedic)
        {
            if (otherBall.useHealth && otherBall.infected && !otherBall.isSuperinfected)
            {
                otherBall.objectAffectingBall = gameObject;
            }
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        BaseBall otherBall = other.GetComponentInParent<BaseBall>();

        if (otherBall == null || !otherBall.useHealth)
                return;

        

        if (infected)
        {
            if (!otherBall.objectAffectingBall && !otherBall.infected)
                otherBall.objectAffectingBall = gameObject;

            float infectChance = m_cachedVirusLevel / (m_cachedVirusLevel + otherBall.m_cachedVirusLevel);
            float dmg = infectChance;


            if (!otherBall.infected)
            {
                otherBall.ChangeHealth(-dmg, gameObject);
                //otherBall.currentHealth -= dmg;
                
                if (otherBall.currentHealth < 0)
                {
                    //Debug.Log("Current Health "+currentHealth);
                    otherBall.SetInfected();
                }

            }else
            {
                if (otherBall.currentHealth > -otherBall.startHealth*0.5f)
                {
                    otherBall.ChangeHealth(-dmg, gameObject);
                    //otherBall.currentHealth -= dmg;
                }
            }
        }else if (isMedic)
        {
            if (otherBall.isSuperinfected)
                return;

            if (!otherBall.objectAffectingBall && otherBall.infected)
                otherBall.objectAffectingBall = gameObject;

            float infectChance = m_cachedVirusLevel / (m_cachedVirusLevel + otherBall.m_cachedVirusLevel);
            float heal = 0.5f;

            if (otherBall.infected)
            {
                otherBall.ChangeHealth(heal, gameObject);
                currentHealth -= heal*2f; //Damage medics when healing other balls
                //otherBall.currentHealth += heal;
                
                if (otherBall.currentHealth > otherBall.startHealth*0.5f)
                {
                    otherBall.SetHealed();
                    //Debug.Log("Current Health Healed "+otherBall.currentHealth);
                }

            }else
            {
                if (otherBall.currentHealth < otherBall.startHealth)
                {
                    otherBall.ChangeHealth(heal, gameObject);
                    //Debug.Log("Healing OtherBall");
                    //otherBall.currentHealth += heal;
                }
            }

            if (currentHealth < 0)
            {
                OnDeath();
            }
            
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        BaseBall otherBall = other.GetComponentInParent<BaseBall>();
        if (useHealth)
        {
            if (otherBall == null)
            {
                return;
            }
            
            if (objectAffectingBall && otherBall.gameObject == objectAffectingBall)
            {
                objectAffectingBall = null;    
            }   
        }
    }

    public virtual void SetInfected()
    {
        if (useHealth)
        {
            if (currentHealth > 0)
            {
                Debug.Log("I still have life in me!");
                return;
            }
        }

        m_cachedTriggerRadius = infectedSetting.triggerRadius;
        m_cachedLifeTime = infectedSetting.lifeTime;
        m_cachedVirusLevel = infectedSetting.virusLevel;
        m_cachedSpeed = infectedSetting.speed;
        GameManager.Instance.scoreManager.IncreaseScore(100);

        myBodyMaterial.material = infectedBodyMaterial;
        myAreaMaterial.material = infectedAreaMaterial;

        infected = true;
        useLifeTime = true;
        
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
        
        StartCoroutine(ScaleToNormal());
    }

    protected virtual void OnDeath()
    {
        dead = true;
    }

    public IEnumerator ScaleUp()
    {
        if (isScaling)
        {
            yield break;
        }

        Vector3 originalSize = triggerArea.transform.localScale;

        isScaling = true;

        yield return pTween.To(animationSpeed, 0, 1, t => {
            triggerArea.transform.localScale = Vector3.Lerp(originalSize, new Vector3(m_cachedTriggerRadius,m_cachedTriggerRadius,m_cachedTriggerRadius), t);
        });

        isScaling = false;

        triggerArea.transform.localScale = originalSize * m_cachedTriggerRadius;
    }

    public IEnumerator DeathAnimation()
    {
        Vector3 originalSize = this.transform.localScale;
        Vector3 targetSize = Vector3.zero;

        yield return pTween.To(animationSpeed, 0, 1, t => 
        {
            this.transform.localScale = Vector3.Lerp(originalSize, targetSize, t);
        });

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

        isScaling = true;

        yield return pTween.To(animationSpeed, 0, 1, t => {
            triggerArea.transform.localScale = Vector3.Lerp(originalSize, targetSize, t);
        });

        isScaling = false;

        triggerArea.transform.localScale = targetSize;
    }

    public void ChangeHealth(float amount, GameObject objectAffecting)
    {
        if (objectAffectingBall == objectAffecting)
            currentHealth += amount;
        return;
    }
}
