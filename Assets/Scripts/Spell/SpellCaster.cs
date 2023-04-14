

public class SpellCaster
{

    public static void Cast(Spell spell)
    {
        if (spell.ProjectileSpeed > 0) Projectile(spell);
        else Raycast(spell);
    }
    
    private static void Projectile(Spell spell)
    {
        
    }

    private static void Raycast(Spell spell)
    {
        
    }

    public enum SpellCastType
    {
        Projectile,
        Raycast
    }
}
