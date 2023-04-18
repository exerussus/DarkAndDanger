

using UnityEngine;

public static class SpellCaster
{
    public static void CastSpell(GameObject caster, Spell spell)
    {
        if (spell.Distance == 0) CastToSelf(caster, spell);
        else if (spell.ProjectileSpeed > 0) CastProjectile(caster, spell);
        else CastRaycast(caster, spell);
    }
    
    private static void CastProjectile(GameObject caster, Spell spell)
    {
        CastingProjectile.Cast(caster, spell);
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
