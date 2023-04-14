
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spell/Spell", fileName = "New spell", order = 51)]
public class Spell : ScriptableObject
{
    [SerializeField] private string spellName;
    [TextArea]
    [SerializeField] private string description;
    [SerializeField] private List<SpellTick> spellTicks;
    
    public string SpellName => spellName;
    public string Description => description;
    public List<SpellTick> SpellTicks => spellTicks;
    
}
