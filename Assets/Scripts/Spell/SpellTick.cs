
using System;
using UnityEngine;

[Serializable]
public class SpellTick
{
    [SerializeField] private float restoreHealth;
    [SerializeField] private float restoreStamina;
    [SerializeField] private float restoreMana;
    
    [SerializeField] private float fire;
    [SerializeField] private float water;
    [SerializeField] private float air;
    [SerializeField] private float earth;
    [SerializeField] private float poison;
    [SerializeField] private float holy;
    [SerializeField] private float necro;
    [SerializeField] private float arcane;    
    
    [SerializeField] private Parameter parameter;
    
    public float RestoreHealth => restoreHealth;
    public float RestoreStamina => restoreStamina;
    public float RestoreMana => restoreMana;
    
    public float Fire =>  fire;
    public float Water => water;
    public float Air =>  air;
    public float Earth => earth;
    public float Poison =>poison;
    public float Holy =>  holy;
    public float Necro => necro;
    public float Arcane => arcane;    
    
    public Parameter Parameter => parameter;
    
}
