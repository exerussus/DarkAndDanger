
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightActionInteraction : IActionAfterInterection
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Light2D light2D;
    [SerializeField] private bool isLightningOnStart = true;
    private bool isLightning = true;

    private void Start()
    {
        light2D = light2D == null?  gameObject.GetComponent<Light2D>() : light2D;
        if (!isLightningOnStart) ChangeLightMode();
    }

    public override void Action(GameObject gameObject)
    {
        ChangeLightMode();
    }

    private void ChangeLightMode()
    {
        light2D.enabled = !light2D.enabled;
        spriteRenderer.enabled = !isLightning;
        isLightning = !isLightning;
    }
}
