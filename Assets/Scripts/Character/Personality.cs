using UnityEngine;

[CreateAssetMenu(menuName = "Parameter/Personality", fileName = "Personality", order = 51)]
public class Personality : ScriptableObject
{
    [SerializeField] private int userID;
    [SerializeField] private string characterName;
    [SerializeField] public Parameter parameter;

    public int UserID => userID;
    public string Name => Name;
    
}