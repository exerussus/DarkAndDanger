
using UnityEngine;

public class InteractionAction : MonoBehaviour
{

    public static InteractionAction instance = null;

    private void Start()
    {
        if (instance == null) instance = this;
        else if (instance == this) Destroy(gameObject);
    }
    
    

    public static void Action(string actionName, GameObject gameObject)
    {
        switch (actionName)
        {


            case "Door":

                
            
            case "Chest":

                
                
                break;
        }


    }
    
}
