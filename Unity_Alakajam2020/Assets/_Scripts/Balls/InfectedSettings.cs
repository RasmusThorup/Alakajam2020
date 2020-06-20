using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class InfectedSettings : ScriptableObject
{
    public float lifeTime = 10;
    public float triggerRadius = 5;
    public int chanceToInfect = 100;
    public int virusResistance = 0;
    
}
