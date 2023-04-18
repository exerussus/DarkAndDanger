
using UnityEngine;

public class TestingSpellCasting : MonoBehaviour
{
    [SerializeField] private KeyboardController keyboardController;
    [SerializeField] private Spell spell;
    [SerializeField] private GameObject caster;

    public void OnEnable()
    {
        keyboardController.OnJump += Cast;
    }

    public void OnDisable()
    {
        keyboardController.OnJump -= Cast;
    }
    
    public void Cast()
    {
        SpellCaster.CastSpell(caster, spell);
    }
}
