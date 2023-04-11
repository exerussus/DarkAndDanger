using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventories", fileName = "Inventories",order = 51)]
public class Inventories : ScriptableObject
{
    public List<Inventory> InventoriesList;
}
