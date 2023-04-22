
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellEffectHandler : MonoBehaviour
{
    private List<SpellAndCount> spellList = new List<SpellAndCount>();
    protected const float DamageDivisor = 50f;
    
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
        spellAndCount.CreateEffects(transform);
        DoEffect(spellAndCount);
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
            if (!spellAndCount.GetIsEnd())
            {
                DoEffect(spellAndCount);
            }
            else
            {
                RemoveSpell(spellAndCount, index);
            }
        }
    }
    
    protected MagicalDamage GetMagicalDamage(SpellTick spellTick, Parameter characterParameter)
    {
        return new MagicalDamage(
            fire: spellTick.Fire + spellTick.Fire / DamageDivisor * characterParameter.fireDamage,
            water: spellTick.Water + spellTick.Water / DamageDivisor * characterParameter.waterDamage,
            air: spellTick.Air + spellTick.Air / DamageDivisor * characterParameter.airDamage,
            earth: spellTick.Earth + spellTick.Earth / DamageDivisor * characterParameter.earthDamage,
            poison: spellTick.Poison + spellTick.Poison / DamageDivisor * characterParameter.poisonDamage,
            holy: spellTick.Holy + spellTick.Holy / DamageDivisor * characterParameter.holyDamage,
            necro: spellTick.Necro + spellTick.Necro / DamageDivisor * characterParameter.necroDamage,
            arcane: spellTick.Arcane + spellTick.Arcane / DamageDivisor * characterParameter.arcaneDamage
        );
    }
    
    protected MagicalDamage GetMagicalResistance(Parameter characterParameter)
    {
        return new MagicalDamage(
            fire: characterParameter.fireResist,
            water: characterParameter.waterResist,
            air: characterParameter.airResist,
            earth: characterParameter.earthResist,
            poison: characterParameter.poisonResist,
            holy: characterParameter.holyResist,
            necro: characterParameter.necroResist,
            arcane: characterParameter.arcaneResist
        );
    }
    
    protected abstract void DoEffect(SpellAndCount spellAndCount);

    protected bool GetIsFirstTick(SpellAndCount spellAndCount)
    {
        return spellAndCount.ActuallyIndex == spellAndCount.Spell.SpellTicks.Count;
    }

    protected abstract void EndEffect(SpellAndCount spellAndCount);

    private void RemoveSpell(SpellAndCount spellAndCount, int index)
    {
        spellAndCount.DestroyEffects();
        EndEffect(spellAndCount);
        spellList.RemoveAt(index);
    }
}

public class SpellAndCount
{
    public Spell Spell { get; }
    public int ActuallyIndex { get; private set; }
    public Parameter CasterParameter { get; private set; }
    public GameObject AdditionalEffect { get; private set; }

    public SpellAndCount(Spell spell, int actuallyIndex, Parameter casterParameter)
    {
        Spell = spell;
        ActuallyIndex = SetIndexFromCount(actuallyIndex);
        CasterParameter = casterParameter;
    }

    public void CreateEffects(Transform handlerTransform)
    {
        AdditionalEffect = Object.Instantiate(original:Spell.AdditionalEffect, parent: handlerTransform);
        AdditionalEffect.transform.SetLocalPositionAndRotation(Vector3.zero, new Quaternion());
    }

    public void DestroyEffects()
    {
        Object.Destroy(AdditionalEffect);
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