
using UnityEngine;

public class DoorActionInteraction : IActionAfterInterection
{
    public override void Action(GameObject gameObject)
    {
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
    }
}
