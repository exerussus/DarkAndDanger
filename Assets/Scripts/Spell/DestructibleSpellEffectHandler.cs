
using UnityEngine;

public class DestructibleSpellEffectHandler : SpellEffectHandler
{
        
    [SerializeField] private DestructibleObject destructibleObject; 
    
    protected override void DoEffect(SpellAndCount spellAndCount)
    {
        if (!GetIsFirstTick(spellAndCount)) EndEffect(spellAndCount);
        
        SpellTick spellTick = spellAndCount.Spell.SpellTicks[^spellAndCount.ActuallyIndex];
        spellAndCount.NextIndex();
        
        var magicDamage = GetMagicalDamage(spellTick, spellAndCount.CasterParameter);
        
        destructibleObject.TakeMagicalDamage(magicDamage);
    }

    protected override void EndEffect(SpellAndCount spellAndCount)
    {

    }
}
