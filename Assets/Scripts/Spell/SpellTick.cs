
using System;
using UnityEngine;

[Serializable]
public class SpellTick
{
    [SerializeField] private float restoreHealth;
    [SerializeField] private float restoreStamina;
    [SerializeField] private float restoreMana;
    [SerializeField] private float magicalDamage;
    [SerializeField] private Parameter parameter;
    
    public float RestoreHealth => restoreHealth;
    public float RestoreStamina => restoreStamina;
    public float RestoreMana => restoreMana;
    public Parameter Parameter => parameter;
    
}
