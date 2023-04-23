
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon/MagicalWeapon", fileName = "MagicalWeapon",order = 51)]
public class MagicalWeapon : Weapon
{
    [SerializeField] private List<Spell> spellList;
    public List<Spell> SpellList => spellList;
}
