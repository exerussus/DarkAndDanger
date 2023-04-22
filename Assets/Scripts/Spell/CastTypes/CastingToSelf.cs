
public static class CastingToSelf
{
    public static void Cast(SpellEffectHandler casterSpellEffectHandler, Character caster, Spell spell)
    {
        casterSpellEffectHandler.AddSpell(spell, caster.Parameter);
    }
}

