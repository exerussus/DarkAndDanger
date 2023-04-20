
using UnityEngine;

public class ChestActionInteraction : IActionAfterInterection
{
    public override void Action(GameObject gameObject)
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        SpriteChanger newSprite = gameObject.GetComponent<SpriteChanger>();
        renderer.sprite = newSprite.SpriteForChange;
    }
}
