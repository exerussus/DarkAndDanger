
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
        Tick.OnTick += DoEffectOnTick;
    }

    private void OnDisable()
    {
        Tick.OnTick -= DoEffectOnTick;
    }

    public void AddSpell(Spell spell)
    {
        spellList.Add(new SpellAndCount(spell:spell, count: spell.SpellTicks.Count));
    }
    
    private void DoEffectOnTick()
    {
        if (spellList.Count == 0) return;


        for (int i = spellList.Count; i > 0; i--)
        {
            SpellAndCount spellAndCount = spellList[i];
            if (spellAndCount.Count != 0)
            {
                DoEffect(spellAndCount);
                spellAndCount.Count -= 1;
            }
            else
            {
                EndEffect(spellAndCount);
                spellList.RemoveAt(i);
            }


        }
    }

    private void DoEffect(SpellAndCount spellAndCount)
    {
        EndEffect(spellAndCount);
        SpellTick spellTick = spellAndCount.Spell.SpellTicks[^spellAndCount.Count];
        
        if(spellTick.RestoreHealth > 0) character.RestoreHealth(spellTick.RestoreHealth);
        if(spellTick.RestoreStamina > 0) character.RestoreStamina(spellTick.RestoreStamina);
        if(spellTick.RestoreMana > 0) character.RestoreMana(spellTick.RestoreMana);
        
    }

    private void EndEffect(SpellAndCount spellAndCount)
    {
        var spellTick = spellAndCount.Spell.SpellTicks[^spellAndCount.Count];
    }
    
}
