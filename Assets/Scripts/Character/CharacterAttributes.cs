using UnityEngine;

[CreateAssetMenu(menuName = "Parameter/Attributes", fileName = "Attributes", order = 51)]
public class CharacterAttributes : ScriptableObject
{
    
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

    [SerializeField] private Parameter strengthParameterScale;
    [SerializeField] private Parameter dexterityParameterScale;
    [SerializeField] private Parameter intelligenceParameterScale;
    [SerializeField] private Parameter constitutionParameterScale;
    
    public Parameter GetParameter()
    {
        var parameter = new Parameter();
        parameter += intelligenceParameterScale * intelligence;
        parameter += strengthParameterScale * strength;
        parameter += dexterityParameterScale * dexterity;
        parameter += constitutionParameterScale * constitution;
        return parameter;
    }
}