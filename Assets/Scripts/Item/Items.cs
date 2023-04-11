using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items", fileName = "Items",order = 51)]
public class Items : ScriptableObject
{
    [SerializeField] private List<Item> itemList;
}
