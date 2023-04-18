
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spell/Spell", fileName = "New spell", order = 51)]
public class Spell : ScriptableObject
{
    [SerializeField] private string spellName;
    [TextArea]
    [SerializeField] private string description;

    [SerializeField] private CastType castType;
    [SerializeField] private int area;
    [SerializeField] private int projectileSpeed;
    [SerializeField] private int distance;
    [SerializeField] private float healthCost;
    [SerializeField] private float staminaCost;
    [SerializeField] private float manaCost;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private List<LayerMask> layerTargets;
    [SerializeField] private List<SpellTick> spellTicks;


    public string SpellName => spellName;
    public string Description => description;
    public CastType CastingType => castType;
    public int Area => area;
    public int ProjectileSpeed => projectileSpeed;    
    public int Distance => distance;    
    public float HealthCost => healthCost;
    public float StaminaCost => staminaCost;
    public float ManaCost => manaCost;
    public GameObject ProjectilePrefab => projectilePrefab;
    public List<LayerMask> LayerTargets => layerTargets;
    public List<SpellTick> SpellTicks => spellTicks;

    public enum CastType
    {
        Self,
        Projectile,
        RayCast
    }
}
