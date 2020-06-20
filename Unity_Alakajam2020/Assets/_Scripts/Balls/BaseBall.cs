using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBall : MonoBehaviour
{

    public InfectedSettings infectedSetup;
    protected float m_cachedLifeTime; 
    protected float m_cachedTriggerRadius;
    protected int m_cachedChanceToInfect = 100;
    protected int m_cachedVirusResistance = 0;
    public GameObject triggerArea;
    public bool infected;
    public bool dead; 
    

    



    private void OnEnable()
    {
        dead = false; 
    }

   public virtual void Update()
    {

        if (Input.anyKey)
        {
            SetInfected();
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

   protected virtual void SetInfected()
   {
       if (infected)
       {
           return;
       }
       m_cachedTriggerRadius = infectedSetup.triggerRadius;
       m_cachedLifeTime = infectedSetup.lifeTime;
       m_cachedVirusResistance = infectedSetup.virusResistance;
       m_cachedChanceToInfect = infectedSetup.chanceToInfect;
       infected = true;
        Debug.Log("Im Infected!");
    }


    protected virtual void OnDeath()
    {
        dead = true; 
        Debug.Log("I Died!");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (infected)
        {
            BaseBall otherBall = other.GetComponent<BaseBall>(); 
            
            //TODO: Calculate if ball should be infected 
            if (!otherBall.infected)
            {
                otherBall.SetInfected();
            }
           
        }
    }
}

