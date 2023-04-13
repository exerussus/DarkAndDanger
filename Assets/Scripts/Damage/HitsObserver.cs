
using System;
using UnityEngine;

public class HitsObserver : MonoBehaviour
{
    public Action<PhysicalDamage, float> OnTakingPhysicalDamage;
    
    public void PhysicalHit(PhysicalDamage damage, float weaponWeight)
    {
        OnTakingPhysicalDamage?.Invoke(damage, weaponWeight);
    }
}
