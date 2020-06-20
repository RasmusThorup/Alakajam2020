//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBall : MonoBehaviour
{
    public InfectedSettings infectedSetup;
    protected float m_cachedLifeTime; 
    protected float m_cachedTriggerRadius;
    protected int m_cachedVirusLevel = 0;
    public int m_cachedSpeed;
    public GameObject triggerArea;
    public bool infected;
    public bool dead;
    public bool debug = false; 


    private void OnEnable()
    {
        dead = false;
        m_cachedSpeed = infectedSetup.speed; 
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

   public virtual void SetInfected()
   {
        m_cachedTriggerRadius = infectedSetup.triggerRadius;
        m_cachedLifeTime = infectedSetup.lifeTime;
        m_cachedVirusLevel = infectedSetup.virusLevel;
        m_cachedSpeed = infectedSetup.speed;

        infected = true;
        //Debug.Log("Im Infected!");
    }


    protected virtual void OnDeath()
    {
        dead = true; 
       // Debug.Log("I Died!");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (infected)
        {
            BaseBall otherBall = other.GetComponentInParent<BaseBall>();

            if (otherBall == null)
            {
                return; 
            }
            
            if (!otherBall.infected)
            {
                int infectChance = m_cachedVirusLevel / (m_cachedVirusLevel + otherBall.m_cachedVirusLevel);
                if (Random.value < infectChance)
                {
                    otherBall.SetInfected();
                }
            }
           
        }
    }
}

