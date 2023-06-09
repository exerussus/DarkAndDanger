
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spell/Spell", fileName = "New spell", order = 51)]
public class Spell : ScriptableObject
{
    [SerializeField] private string spellName;
    [TextArea][SerializeField] private string description;
    [SerializeField] private CastType castType;
    [SerializeField] private int area;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private int distance;
    [SerializeField] private float healthCost;
    [SerializeField] private float staminaCost;
    [SerializeField] private float manaCost;
    [SerializeField] private float timeCastCost;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Sprite icon;
    [SerializeField] private LayerMask layerTargets;
    [SerializeField] private GameObject castEffect;
    [SerializeField] private GameObject mainEffect;
    [SerializeField] private GameObject additionalEffect;
    [SerializeField] private List<SpellTick> spellTicks;


    public string SpellName => spellName;
    public string Description => description;
    public CastType CastingType => castType;
    public int Area => area;
    public float ProjectileSpeed => projectileSpeed;    
    public int Distance => distance;    
    public float HealthCost => healthCost;
    public float StaminaCost => staminaCost;
    public float ManaCost => manaCost;
    public float TimeCastCost => timeCastCost;
    public GameObject ProjectilePrefab => projectilePrefab;
    public Sprite Icon => icon;
    public LayerMask LayerTargets => layerTargets;
    public GameObject CastEffect => castEffect;
    public GameObject MainEffect => mainEffect;
    public GameObject AdditionalEffect => additionalEffect;
    public List<SpellTick> SpellTicks => spellTicks;

    public enum CastType
    {
        Self,
        Projectile,
        RayCast
    }
}
