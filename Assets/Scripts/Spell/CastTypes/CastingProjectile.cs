
using UnityEngine;

public static class CastingProjectile
{
    public static void Cast(Transform casterTransform, Character caster, Spell spell)
    {
        var projectile = Object.Instantiate(spell.ProjectilePrefab);
        projectile.transform.SetPositionAndRotation(caster.transform.position, casterTransform.rotation);
        projectile.transform.localPosition += caster.transform.TransformDirection(new Vector3(0f, 1f, 0f));
        var spellProjectile = projectile.GetComponent<SpellProjectile>();
        spellProjectile.SetCaster(caster);
        spellProjectile.SetSpell(spell);
        spellProjectile.Activate();
    }
}
