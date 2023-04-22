
using UnityEngine;

public class Weapon : ScriptableObject
{
    [SerializeField] private Item item;
    [SerializeField] private GameObject prefab;
    public Item Item => item;
    public GameObject Prefab => prefab;
}