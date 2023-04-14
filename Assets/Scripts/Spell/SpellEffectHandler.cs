
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectHandler : MonoBehaviour
{
    [SerializeField] private Character character;
    private List<SpellAndCount> spellList;

    public struct SpellAndCount
    {
        public Spell Spell { get; }
        private int count;
        public int Count
        {
            get => count;
            set => count = value >= 0 ? value : 0;
        }

        public SpellAndCount(Spell spell, int count)
        {
            Spell = spell;
            this.count = count;
        }
    }
    
    private void OnEnable()
    {
        Tick.OnTick += DoTickEffect;
    }

    private void OnDisable()
    {
        Tick.OnTick -= DoTickEffect;
    }

    public void AddSpell(Spell spell)
    {
        spellList.Add(new SpellAndCount(spell:spell, count: spell.SpellTicks.Count));
    }
    
    private void DoTickEffect()
    {
        if (spellList.Count == 0) return;

        foreach (var spell in spellList)
        {
            
        }
    }
    
}
