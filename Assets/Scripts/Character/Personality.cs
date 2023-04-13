
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Parameter/Personality", fileName = "Personality", order = 51)]
public class Personality : ScriptableObject
{
    [SerializeField] public int UserID;
    [SerializeField] public string CharacterName;
    [SerializeField] private Parameter parameter;
    public Parameter Parameter => parameter;

    public Action OnRecalculateParameter;

    [SerializeField] private int strength;
    public int Strength
    {
        get => strength;
        set => strength = value >= 0 ? value : 0;
    }
    
    [SerializeField] private int dexterity;
    public int Dexterity
    {
        get => dexterity;
        set => dexterity = value >= 0 ? value : 0;
    }
    
    [SerializeField] private int intelligence;
    public int Intelligence
    {
        get => intelligence;
        set => intelligence = value >= 0 ? value : 0;
    }
    
    [SerializeField] private int constitution;
    public int Constitution
    {
        get => constitution;
        set => constitution = value >= 0 ? value : 0;
    }


    public string Name => Name;

    public void RecalculateParameter()
    {
        parameter = new Parameter();
        OnRecalculateParameter?.Invoke();
    }

    public void AddParameterToCharacter(Parameter parameter)
    {
        this.parameter += parameter;
    }

}