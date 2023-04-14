﻿
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
        var spellAndCount = new SpellAndCount(spell: spell, count: spell.SpellTicks.Count);
        DoEffect(spellAndCount);
        spellList.Add(spellAndCount);
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
            }
            else
            {
                RemoveSpell(spellAndCount, i);
            }
        }
    }

    private void DoEffect(SpellAndCount spellAndCount)
    {
        SpellTick spellTick = spellAndCount.Spell.SpellTicks[^spellAndCount.Count];
        if (spellAndCount.Count != spellAndCount.Spell.SpellTicks.Count) EndEffect(spellAndCount);
        spellAndCount.Count -= 1;
        
        if(spellTick.RestoreHealth > 0) character.RestoreHealth(spellTick.RestoreHealth);
        if(spellTick.RestoreStamina > 0) character.RestoreStamina(spellTick.RestoreStamina);
        if(spellTick.RestoreMana > 0) character.RestoreMana(spellTick.RestoreMana);
        
        // To do: add magic damage calculation
        
        character.Personality.AddParameterToCharacter(spellTick.Parameter);
        
    }

    private void EndEffect(SpellAndCount spellAndCount)
    {
        var spellTick = spellAndCount.Spell.SpellTicks[^spellAndCount.Count];
        character.Personality.SubtractParameterFromCharacter(spellTick.Parameter);
        
    }

    private void RemoveSpell(SpellAndCount spellAndCount, int index)
    {
        EndEffect(spellAndCount);
        spellList.RemoveAt(index);
    }
}