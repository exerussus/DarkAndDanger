
using UnityEngine;

public static class SpellCaster
{
    public static void CastSpell(GameObject caster, Spell spell)
    {
        if (spell.CastingType == Spell.CastType.Self) CastToSelf(caster, spell);
        else if (spell.CastingType == Spell.CastType.Projectile) CastProjectile(spell);
        else CastRaycast(caster, spell);
    }
    
    private static void CastProjectile(Spell spell)
    {
        CastingProjectile.Cast(spell);
    }

    private static void CastRaycast(GameObject caster, Spell spell)
    {
        CastingRaycast.Cast(caster, spell);
    }

    private static void CastToSelf(GameObject caster, Spell spell)
    {
        CastingToSelf.Cast(caster, spell);
    }
}
