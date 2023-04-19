
using UnityEngine;

public static class CastingRaycast
{
    public static void Cast(Transform casterTransform, Character caster, Spell spell)
    {
        // foreach (var layerTarget in spell.LayerTargets)
        // {
            RaycastHit2D hit = Physics2D.Raycast(casterTransform.position, caster.transform.up, spell.Distance, spell.LayerTargets);
            if (hit.collider != null)
            {
                
                var spellEffectHandler = caster.GetComponent<SpellEffectHandler>();
                spellEffectHandler.AddSpell(spell, caster.Parameter);
            }
        // }
    }
}
