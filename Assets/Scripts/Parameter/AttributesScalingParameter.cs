using UnityEngine;


[CreateAssetMenu(menuName = "Parameter/AttributesScalingParameter", fileName = "AttributesScalingParameter", order = 51)]
public class AttributesScalingParameter : ScriptableObject
{
    [SerializeField] private Parameter strengthParameterScale;    
    [SerializeField] private Parameter dexterityParameterScale;    
    [SerializeField] private Parameter constitutionParameterScale;
    [SerializeField] private Parameter intelligenceParameterScale;
    
    public Parameter StrengthParameterScale => strengthParameterScale;    
    public Parameter DexterityParameterScale => dexterityParameterScale;    
    public Parameter ConstitutionParameterScale => constitutionParameterScale;
    public Parameter IntelligenceParameterScale => intelligenceParameterScale;
}
