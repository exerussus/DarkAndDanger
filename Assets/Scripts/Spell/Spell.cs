
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spell/Spell", fileName = "New spell", order = 51)]
public class Spell : ScriptableObject
{
    [SerializeField] private string spellName;
    [TextArea]
    [SerializeField] private string description;
    [SerializeField] private int area;
    [SerializeField] private int projectileSpeed;
    [SerializeField] private float healthCost;
    [SerializeField] private float staminaCost;
    [SerializeField] private float manaCost;  
    [SerializeField] private List<SpellTick> spellTicks;


    public string SpellName => spellName;
    public string Description => description;
    public int Area => area;
    public int ProjectileSpeed => projectileSpeed;    
    public float HealthCost => healthCost;
    public float StaminaCost => staminaCost;
    public float ManaCost => manaCost;
    public List<SpellTick> SpellTicks => spellTicks;
}
