using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class InfectedSettings : ScriptableObject
{
    public float lifeTime = 10;
    public float triggerRadius = 5;
    public int virusLevel = 100;
    public int speed = 5;


    public void SetLifeTime(float value)
    {
        lifeTime += value;
        if (lifeTime <= 0)
        {
            lifeTime = 1; 
        }
        
    }

    public void SetTriggerRadius(float value)
    {
        triggerRadius += value;
        if (triggerRadius <= 0)
        {
            triggerRadius = 1; 
        }
        
    }
    
    
    public void SetVirusLevel(int value)
    {
        virusLevel += value;
        if (virusLevel <= 0)
        {
            virusLevel = 1; 
        }
    }
    
    public void SetSpeed(int value)
    {
        speed += value;
        if (speed <= 0)
        {
            speed = 1; 
        }
    }
    
    
    
}
