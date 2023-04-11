
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

                Transform oldTransform = gameObject.GetComponent<Transform>();
                Transform parentTransform = oldTransform.parent;
                ReplacementObject replacementObject = gameObject.GetComponent<ReplacementObject>();
                GameObject newGameObject = replacementObject.NewGameObject;
                Vector3 oldPosition = oldTransform.localPosition;
                var resultPosition = parentTransform.position + oldPosition + replacementObject.CorrectionVector;
                Quaternion oldQuaternion = oldTransform.rotation;

                Destroy(gameObject);
                var createdObject = Instantiate(original: newGameObject, position: resultPosition,
                    rotation: oldQuaternion, parent: parentTransform);
                break;
            
            case "Chest":

                SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
                SpriteChanger newSprite = gameObject.GetComponent<SpriteChanger>();
                renderer.sprite = newSprite.SpriteForChange;
                
                break;
        }


    }
    
}
