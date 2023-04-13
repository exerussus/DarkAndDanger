
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Parameter/Personality", fileName = "Personality", order = 51)]
public class Personality : ScriptableObject
{
    [SerializeField] public int UserID;
    [SerializeField] public string CharacterName;
    [SerializeField] private Parameter parameter;
    [SerializeField] private Parameter StandardParameter;
    [SerializeField] private CharacterAttributes characterAttributes;
    public Parameter Parameter => parameter;

    public Action OnRecalculateParameter;

    public string Name => Name;

    public void RecalculateParameter()
    {
        parameter = new Parameter();
        OnRecalculateParameter?.Invoke();
        AddParameterToCharacter(StandardParameter);
        AddParameterToCharacter(characterAttributes.GetParameter());
    }

    public void AddParameterToCharacter(Parameter parameter)
    {
        this.parameter += parameter;
    }

}