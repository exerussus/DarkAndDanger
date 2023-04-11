using UnityEngine;

public class CharactersData : MonoBehaviour
{
    [SerializeField] private CharactersParameterData characterParametersData;

    public Parameter GetUserParameter(int userID)
    {
        foreach (var UserIDAndParameter in characterParametersData.characterParameter)
        {
            if (UserIDAndParameter.UserID == userID) return UserIDAndParameter.parameter;
        }

        characterParametersData.characterParameter.Add(new CharactersParameter(userID));

        return new Parameter();
    }
}