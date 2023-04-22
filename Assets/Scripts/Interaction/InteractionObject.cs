
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(IActionAfterInterection))]
public class InteractionObject : MonoBehaviour
{
    [SerializeField] private float timeCost;
    [SerializeField] private IActionAfterInterection actionInteraction;
    [SerializeField] private AudioClip audioClip;

    public AudioClip AudioClip => audioClip;
    public float TimeCost => timeCost;

    private void Start()
    {
        actionInteraction = actionInteraction == null ? GetComponent<IActionAfterInterection>() : actionInteraction;
    }

    public void InteractionCompleted()
    {
        actionInteraction.Action();
    }
}
