
using UnityEngine;

public class CastingProjectile : MonoBehaviour
{
    public static void Cast(GameObject caster, Spell spell)
    {
        var projectile = Instantiate(spell.ProjectilePrefab);
        projectile.transform.SetPositionAndRotation(caster.transform.position, caster.transform.rotation);
        projectile.transform.localPosition += caster.transform.TransformDirection(new Vector3(0f, 1f, 0f));
        var spellProjectile = projectile.GetComponent<SpellProjectile>();
        spellProjectile.SetSpell(spell);
        spellProjectile.Activate();
    }
}
