
using UnityEngine;

public class CharacterSpellEffectHandler : SpellEffectHandler
{
    
    [SerializeField] private Character character; 
    
    
     protected override void DoEffect(SpellAndCount spellAndCount)
    {
        if (!GetIsFirstTick(spellAndCount)) EndEffect(spellAndCount);
        
        SpellTick spellTick = spellAndCount.Spell.SpellTicks[^spellAndCount.ActuallyIndex];
        spellAndCount.NextIndex();

        if(spellTick.RestoreHealth > 0) character.RestoreHealth(spellTick.RestoreHealth);
        if(spellTick.RestoreStamina > 0) character.RestoreStamina(spellTick.RestoreStamina);
        if(spellTick.RestoreMana > 0) character.RestoreMana(spellTick.RestoreMana);

        var magicResistance = GetMagicalResistance(character.Parameter);
        var magicDamage = GetMagicalDamage(spellTick, spellAndCount.CasterParameter);
        
        character.TakeMagicalDamage(magicDamage.GetDamageWithResistance(magicResistance));
        character.Personality.AddParameterToCharacter(spellTick.Parameter);
    }


     
     protected override void EndEffect(SpellAndCount spellAndCount)
     {
         var spellTick = spellAndCount.Spell.SpellTicks[^spellAndCount.GetIndexBefore()];
         character.Personality.SubtractParameterFromCharacter(spellTick.Parameter);
     }
}
