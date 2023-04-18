
using UnityEngine;

public static class CastingRaycast
{
    public static void Cast(GameObject caster, Spell spell)
    {
        foreach (var layerTarget in spell.LayerTargets)
        {
            RaycastHit2D hit = Physics2D.Raycast(caster.transform.position, caster.transform.up, spell.Distance, layerTarget);
            if (hit.collider != null)
            {
                
                var spellEffectHandler = caster.GetComponent<SpellEffectHandler>();
                spellEffectHandler.AddSpell(spell);
            }
        }
    }
}
