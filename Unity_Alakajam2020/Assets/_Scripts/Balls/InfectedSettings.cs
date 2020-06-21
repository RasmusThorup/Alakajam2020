using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class InfectedSettings : ScriptableObject
{

    [SerializeField] private float baseLifeTime = 10f;
    [SerializeField] private float baseTriggerRadius = 5f;
    [SerializeField] private int baseVirusLevel = 100;
    [SerializeField] private int baseSpeed = 5;

    [HideInInspector] public float lifeTime;
    [HideInInspector] public float triggerRadius;
    [HideInInspector] public int virusLevel;
    [HideInInspector] public int speed;

    // Initialize coolDown with editor's value
    private void OnEnable()
    {
        lifeTime = baseLifeTime;
        triggerRadius = baseTriggerRadius;
        virusLevel = baseVirusLevel;
        speed = baseSpeed;
    }

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
