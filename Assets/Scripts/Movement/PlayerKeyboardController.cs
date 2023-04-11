using UnityEngine;

public class PlayerKeyboardController : KeyboardController
{
    protected override bool IsPressRight()
    {
        return Input.GetKey(KeyCode.D);
    }

    protected override bool IsPressLeft()
    {
        return Input.GetKey(KeyCode.A);
    }

    protected override bool IsPressForward()
    {
        return Input.GetKey(KeyCode.W);
    }

    protected override bool IsPressBack()
    {
        return Input.GetKey(KeyCode.S);
    }

    protected override bool IsPressJump()
    {
        return Input.GetKey(KeyCode.Space);
    }

    protected override bool IsPressInteract()
    {
        return Input.GetKeyDown(KeyCode.F);
    }

    protected override bool IsPressSprint()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    protected override bool IsPressCrouch()
    {
        return Input.GetKey(KeyCode.C);
    }

    protected override bool IsPressFirstAttack()
    {
        return Input.GetAxis("Mouse ScrollWheel") > 0f;
    }

    protected override bool IsPressSecondAttack()
    {
        return Input.GetAxis("Mouse ScrollWheel") < 0f;
    }

    protected override bool IsPressThirdAttack()
    {
        return Input.GetMouseButton(0);
    }

    protected override bool IsDownParry()
    {
        return Input.GetMouseButtonDown(1);
    }

    protected override bool IsUpParry()
    {
        return Input.GetMouseButtonUp(1);
    }
}
