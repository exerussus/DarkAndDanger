using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parameter/Perks", fileName = "Perks",order = 51)]
public class Perks : ScriptableObject
{
    [SerializeField] private List<Perk> perkList;
    
}
