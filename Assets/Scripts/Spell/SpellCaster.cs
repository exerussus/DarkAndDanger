
using UnityEngine;

public static class SpellCaster
{
    public static void CastSpell(Transform casterTransform, Character caster, Spell spell)
    {
        if (spell.CastingType == Spell.CastType.Self) CastToSelf(caster, spell);
        else if (spell.CastingType == Spell.CastType.Projectile) CastProjectile(casterTransform, caster, spell);
        else CastRaycast(casterTransform, caster, spell);
    }
    
    private static void CastProjectile(Transform casterTransform, Character caster, Spell spell)
    {
        CastingProjectile.Cast(casterTransform, caster, spell);
    }

    private static void CastRaycast(Transform casterTransform, Character caster, Spell spell)
    {
        CastingRaycast.Cast(casterTransform, caster, spell);
    }

    private static void CastToSelf(Character caster, Spell spell)
    {
        CastingToSelf.Cast(caster, spell);
    }
}
