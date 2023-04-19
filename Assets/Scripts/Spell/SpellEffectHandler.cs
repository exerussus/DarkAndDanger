
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectHandler : MonoBehaviour
{
    [SerializeField] private Character character; 
    private List<SpellAndCount> spellList = new List<SpellAndCount>();

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
        var spellAndCount = new SpellAndCount(spell: spell, actuallyIndex: spell.SpellTicks.Count);
        DoEffect(spellAndCount);
        spellList.Add(spellAndCount);
    }
    
    private void DoEffectOnTick()
    {
        if (spellList.Count == 0) return;
        
        for (int i = spellList.Count; i > 0; i--)
        {
            var index = i - 1;
            SpellAndCount spellAndCount = spellList[index];
            Debug.Log("spellAndCount.Count: " + spellAndCount.ActuallyIndex);
            if (!spellAndCount.GetIsEnd()) DoEffect(spellAndCount);
            else RemoveSpell(spellAndCount, index);
        }
    }

    private void DoEffect(SpellAndCount spellAndCount)
    {
        Debug.Log("spellAndCount.Spell.SpellTicks.Count: " + spellAndCount.Spell.SpellTicks.Count);
        Debug.Log("spellAndCount.ActuallyIndex: " + spellAndCount.ActuallyIndex);
        if (!GetIsFirstTick(spellAndCount)) EndEffect(spellAndCount);
        
        SpellTick spellTick = spellAndCount.Spell.SpellTicks[^spellAndCount.ActuallyIndex];

        if(spellTick.RestoreHealth > 0) character.RestoreHealth(spellTick.RestoreHealth);
        if(spellTick.RestoreStamina > 0) character.RestoreStamina(spellTick.RestoreStamina);
        if(spellTick.RestoreMana > 0) character.RestoreMana(spellTick.RestoreMana);

        var magicDamage = new MagicalDamage(
            fire: character.Parameter.fireResist,
            water: character.Parameter.waterResist,
            air: character.Parameter.airResist,
            earth: character.Parameter.earthResist,
            poison:character.Parameter.poisonResist,
            holy: character.Parameter.holyResist,
            necro: character.Parameter.necroResist,
            arcane:character.Parameter.arcaneResist
        );
        
        var magicResistance = new MagicalDamage(
            fire: spellTick.Fire,
            water: spellTick.Water,
            air: spellTick.Air,
            earth: spellTick.Earth,
            poison: spellTick.Poison,
            holy: spellTick.Holy,
            necro: spellTick.Necro,
            arcane: spellTick.Arcane
        );
        
        character.TakeMagicalDamage(magicDamage.GetDamageWithResistance(magicResistance));
        character.Personality.AddParameterToCharacter(spellTick.Parameter);
        spellAndCount.NextIndex();
    }

    private bool GetIsFirstTick(SpellAndCount spellAndCount)
    {
        return spellAndCount.ActuallyIndex == spellAndCount.Spell.SpellTicks.Count -1;
    }
    
    private void EndEffect(SpellAndCount spellAndCount)
    {
        var spellTick = spellAndCount.Spell.SpellTicks[^spellAndCount.ActuallyIndex];
        character.Personality.SubtractParameterFromCharacter(spellTick.Parameter);
        
    }

    private void RemoveSpell(SpellAndCount spellAndCount, int index)
    {
        EndEffect(spellAndCount);
        spellList.RemoveAt(index);
    }
}

public class SpellAndCount
{
    public Spell Spell { get; }
    public int ActuallyIndex { get; private set; }

    public SpellAndCount(Spell spell, int actuallyIndex)
    {
        Spell = spell;
        ActuallyIndex = SetIndexFromCount(actuallyIndex);
    }

    public bool GetIsEnd()
    {
        return ActuallyIndex <= 1;
    }
    
    public void NextIndex()
    {
        ActuallyIndex -= 1;
    }
    
    public int SetIndexFromCount(int count)
    {
        return count;
    }
    
}