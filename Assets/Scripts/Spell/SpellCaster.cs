
using UnityEngine;

public static class SpellCaster
{
    public static void CastSpell(Transform casterTransform, Character casterCharacter, Spell spell, SpellEffectHandler casterSpellEffectHandler)
    {
        if (spell.CastingType == Spell.CastType.Self) CastToSelf(casterSpellEffectHandler, casterCharacter, spell);
        else if (spell.CastingType == Spell.CastType.Projectile) CastProjectile(casterTransform, casterCharacter, spell);
        else if (spell.CastingType == Spell.CastType.RayCast) CastRaycast(casterTransform, casterCharacter, spell);
    }
    
    private static void CastProjectile(Transform casterTransform, Character caster, Spell spell)
    {
        CastingProjectile.Cast(casterTransform, caster, spell);
    }

    private static void CastRaycast(Transform casterTransform, Character caster, Spell spell)
    {
        CastingRaycast.Cast(casterTransform, caster, spell);
    }

    private static void CastToSelf(SpellEffectHandler casterSpellEffectHandler,Character casterCharacter, Spell spell)
    {
        CastingToSelf.Cast(casterSpellEffectHandler, casterCharacter, spell);
    }
}
