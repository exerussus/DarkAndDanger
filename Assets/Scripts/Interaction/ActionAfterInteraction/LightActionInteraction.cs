
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightActionInteraction : IActionAfterInterection
{
    [SerializeField] private Light2D light2D;

    private void Start()
    {
        light2D = light2D == null?  gameObject.GetComponent<Light2D>() : light2D;
    }

    public override void Action(GameObject gameObject)
    {
        light2D.enabled = !light2D.enabled;
    }
}
