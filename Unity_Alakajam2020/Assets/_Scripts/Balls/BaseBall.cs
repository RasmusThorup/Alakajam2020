using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBall : MonoBehaviour
{

    public float lifeTime = 10;
    protected float m_chachedLifetime;
    public float triggerRadius = 10;
    public GameObject triggerArea;
    public bool infected;
    public bool dead; 
    public int chanceToInfect;
    public int virusResistance;


    private void OnEnable()
    {
   
        m_chachedLifetime = lifeTime;
        dead = false; 
    }

   public virtual void Update()
    {

        if (Input.anyKey)
        {
            OnInfected(); 
        }
        if (infected)
        {
            if (m_chachedLifetime <= 0)
            {
                OnDeath();
            }

            m_chachedLifetime -= Time.deltaTime;
        }
        
    }

   protected virtual void OnInfected()
    {
        infected = true;
        Debug.Log("Im Infected!");
    }


    protected virtual void OnDeath()
    {
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
                otherBall.OnInfected();
            }
           
        }
    }
}

