
using UnityEngine;


[CreateAssetMenu(menuName = "Parameter/StandardParameter", fileName = "StandardParameter", order = 51)]
public class StandardParameter : ScriptableObject
{
    [SerializeField] private Parameter standardParameter;

    public Parameter GetParameter()
    {
        return standardParameter;
    }
}
