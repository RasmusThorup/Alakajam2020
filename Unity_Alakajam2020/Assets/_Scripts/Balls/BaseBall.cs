using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBall : MonoBehaviour
{

    public float lifeTime = 10;
    private float m_chachedLifetime;
    public float triggerRadius = 10;
    public SphereCollider triggerCollider;
    public bool infected;
    public bool dead; 
    public int chanceToInfect;
    public int virusResistance;


    private void OnEnable()
    {
        triggerCollider.radius = triggerRadius; 
        m_chachedLifetime = lifeTime;
        dead = false; 
    }

    void Update()
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
            Debug.Log(m_chachedLifetime);
        }
        
    }

    public virtual void OnInfected()
    {

        infected = true; 
    }


    public virtual void OnDeath()
    {
        dead = true; 
        this.gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {

        if (infected)
        {
            BaseBall otherBall = other.GetComponent<BaseBall>(); 
            
            //TODO: Calculate if ball should be infected 
            
            otherBall.OnInfected();
        }
    }
}

