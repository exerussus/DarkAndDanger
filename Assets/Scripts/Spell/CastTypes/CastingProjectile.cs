
using UnityEngine;

public class CastingProjectile : MonoBehaviour
{
    public static void Cast(Spell spell)
    {
        var projectile = Instantiate(spell.ProjectilePrefab);
        var spellProjectile = projectile.GetComponent<SpellProjectile>();
        spellProjectile.SetSpell(spell);
        spellProjectile.Activate();
    }
}
