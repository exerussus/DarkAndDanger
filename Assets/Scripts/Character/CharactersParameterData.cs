


using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parameter/CharactersParameterData", fileName = "CharactersParameterData", order = 51)]
public class CharactersParameterData : ScriptableObject
{
    public List<CharactersParameter> characterParameter;

}

[Serializable]
public class CharactersParameter
{
    [SerializeField] private int userID;
    public Parameter parameter;

    public int UserID => userID;

    public CharactersParameter(int userID)
    {
        this.userID = userID;
        parameter = new Parameter();
    }
}