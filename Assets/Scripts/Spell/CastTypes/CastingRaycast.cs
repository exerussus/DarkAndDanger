
using UnityEngine;

public static class CastingRaycast
{

    public static void Cast(Transform casterTransform, Character caster, Spell spell)
    {
        RaycastHit2D hit = Physics2D.Raycast(
            casterTransform.position + casterTransform.TransformDirection(0f, 1f, 0f), 
            caster.transform.up, 
            spell.Distance, 
            spell.LayerTargets);
        
        if (hit.collider == null) return;
        var spellEffectHandler = hit.collider.GetComponent<SpellEffectHandler>();
        spellEffectHandler.AddSpell(spell, caster.Parameter);
        BlowEffect(spell, hit.collider.transform);
    }
    private static void BlowEffect(Spell spell, Transform transform)
    {
        var mainEffect = Object.Instantiate(spell.MainEffect);
        mainEffect.transform.position = transform.position;
    }
}
