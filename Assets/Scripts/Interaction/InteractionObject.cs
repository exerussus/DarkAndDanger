
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private float timeCost;
    [SerializeField] private IActionAfterInterection actionInteraction;
    [SerializeField] private new GameObject gameObject;
    public float TimeCost => timeCost;

    private void Start()
    {
        gameObject = gameObject == null ? GetComponent<GameObject>() : gameObject;
    }

    public void InteractionCompleted()
    {
        actionInteraction.Action(gameObject);
    }
}
