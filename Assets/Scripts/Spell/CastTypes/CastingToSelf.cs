
using UnityEngine;

public static class CastingToSelf
{
    public static void Cast(Character caster, Spell spell)
    {
        var spellEffectHandler = caster.GetComponentInChildren<SpellEffectHandler>();
        spellEffectHandler.AddSpell(spell, caster.Parameter);
    }
}

