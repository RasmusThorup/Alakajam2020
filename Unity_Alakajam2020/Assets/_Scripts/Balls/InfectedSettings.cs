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
    
}
