
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectHandler : MonoBehaviour
{
    [SerializeField] private Character character; 
    private List<SpellAndCount> spellList = new List<SpellAndCount>();
    public const float DamageDivisor = 50f;
    
    private void OnEnable()
    {
        Tick.OnTick += DoEffectOnTick;
    }

    private void OnDisable()
    {
        Tick.OnTick -= DoEffectOnTick;
    }

    public void AddSpell(Spell spell, Parameter casterParameter)
    {
        var spellAndCount = new SpellAndCount(spell: spell, actuallyIndex: spell.SpellTicks.Count, casterParameter);
        DoEffect(spellAndCount);
        Debug.Log("AddSpell.spellAndCount.ActuallyIndex: " + spellAndCount.ActuallyIndex);
        if(spellAndCount.ActuallyIndex > 0) spellList.Add(spellAndCount);
        else EndEffect(spellAndCount);
    }
    
    private void DoEffectOnTick()
    {
        if (spellList.Count == 0) return;
        
        for (int i = spellList.Count; i > 0; i--)
        {
            var index = i - 1;
            SpellAndCount spellAndCount = spellList[index];
            Debug.Log("spellAndCount.ActuallyIndex: " + spellAndCount.ActuallyIndex);
            if (!spellAndCount.GetIsEnd())
            {
                DoEffect(spellAndCount);
            }
            else
            {
                RemoveSpell(spellAndCount, index);
                Debug.Log("Spell удалён");
                Debug.Log("spellList.Count: " + spellList.Count);
            }
        }
    }

    private void DoEffect(SpellAndCount spellAndCount)
    {
        if (!GetIsFirstTick(spellAndCount)) EndEffect(spellAndCount);
        
        SpellTick spellTick = spellAndCount.Spell.SpellTicks[^spellAndCount.ActuallyIndex];
        spellAndCount.NextIndex();

        if(spellTick.RestoreHealth > 0) character.RestoreHealth(spellTick.RestoreHealth);
        if(spellTick.RestoreStamina > 0) character.RestoreStamina(spellTick.RestoreStamina);
        if(spellTick.RestoreMana > 0) character.RestoreMana(spellTick.RestoreMana);

        var magicResistance = new MagicalDamage(
            fire: character.Parameter.fireResist,
            water: character.Parameter.waterResist,
            air: character.Parameter.airResist,
            earth: character.Parameter.earthResist,
            poison:character.Parameter.poisonResist,
            holy: character.Parameter.holyResist,
            necro: character.Parameter.necroResist,
            arcane:character.Parameter.arcaneResist
        );
        
        var magicDamage = new MagicalDamage(
            fire: spellTick.Fire + spellTick.Fire / DamageDivisor * spellAndCount.CasterParameter.fireDamage,
            water: spellTick.Water + spellTick.Water / DamageDivisor * spellAndCount.CasterParameter.waterDamage,
            air: spellTick.Air + spellTick.Air / DamageDivisor * spellAndCount.CasterParameter.airDamage,
            earth: spellTick.Earth + spellTick.Earth / DamageDivisor * spellAndCount.CasterParameter.earthDamage,
            poison: spellTick.Poison + spellTick.Poison / DamageDivisor * spellAndCount.CasterParameter.poisonDamage,
            holy: spellTick.Holy + spellTick.Holy / DamageDivisor * spellAndCount.CasterParameter.holyDamage,
            necro: spellTick.Necro + spellTick.Necro / DamageDivisor * spellAndCount.CasterParameter.necroDamage,
            arcane: spellTick.Arcane + spellTick.Arcane / DamageDivisor * spellAndCount.CasterParameter.arcaneDamage
        );
        
        character.TakeMagicalDamage(magicDamage.GetDamageWithResistance(magicResistance));
        character.Personality.AddParameterToCharacter(spellTick.Parameter);
    }

    private bool GetIsFirstTick(SpellAndCount spellAndCount)
    {
        return spellAndCount.ActuallyIndex == spellAndCount.Spell.SpellTicks.Count;
    }
    
    private void EndEffect(SpellAndCount spellAndCount)
    {
        var spellTick = spellAndCount.Spell.SpellTicks[^spellAndCount.GetIndexBefore()];
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
    public Parameter CasterParameter { get; private set; }

    public SpellAndCount(Spell spell, int actuallyIndex, Parameter casterParameter)
    {
        Spell = spell;
        ActuallyIndex = SetIndexFromCount(actuallyIndex);
        CasterParameter = casterParameter;
    }

    public int GetIndexBefore()
    {
        return ActuallyIndex + 1;
    }
    
    public bool GetIsEnd()
    {
        return ActuallyIndex < 1;
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