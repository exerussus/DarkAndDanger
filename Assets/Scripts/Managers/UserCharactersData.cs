
using UnityEngine;

public class UserCharactersData : MonoBehaviour
{
    public static UserCharactersData instance = null;
    private static CharactersData charactersData;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            charactersData = GetComponent<CharactersData>();
        }
        else if(instance == this) Destroy(gameObject);
    }

    public static Parameter GetUserParameter(int userID)
    {
        return charactersData.GetUserParameter(userID);
    }
}
