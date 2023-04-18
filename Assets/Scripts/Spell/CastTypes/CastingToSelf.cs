
using UnityEngine;

public static class CastingToSelf
{
    public static void Cast(GameObject caster, Spell spell)
    {
        var spellEffectHandler = caster.GetComponent<SpellEffectHandler>();
        spellEffectHandler.AddSpell(spell);
    }
}

