
using UnityEngine;

public class DoorActionInteraction : IActionAfterInterection
{
    [SerializeField] private ReplacementObject replacementObject;
    
    private void Start()
    {
        replacementObject = replacementObject == null?  gameObject.GetComponent<ReplacementObject>() : replacementObject;
    }

    public override void Action(GameObject gameObject)
    {
        Transform oldTransform = gameObject.GetComponent<Transform>();
        Transform parentTransform = oldTransform.parent;
        GameObject newGameObject = replacementObject.NewGameObject;
        Vector3 oldPosition = oldTransform.localPosition;
        var resultPosition = parentTransform.position + oldPosition + replacementObject.CorrectionVector;
        Quaternion oldQuaternion = oldTransform.rotation;

        Destroy(gameObject);
        Instantiate(original: newGameObject, position: resultPosition,
            rotation: oldQuaternion, parent: parentTransform);
    }
}
