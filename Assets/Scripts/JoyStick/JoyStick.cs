using UnityEngine.UI; 
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    [SerializeField] RectTransform joystickPoint;

    private RectTransform joystick;
    private Vector2 mousePos = Vector2.zero;

    private Image color;
    private Image joystickPointColor;
    private Color newAlphaColor;

    private int transparent = 0;
    private int opaque = 255;


    private void OnEnable()
    {
        InputController.TapScreen += SetJoystickPosition;
        InputController.EndTapScreen += InputController_EndTapScreen;

        joystick = GetComponent<RectTransform>();
        color = GetComponent<Image>();
        joystickPointColor = joystickPoint.GetComponent<Image>();

        SetJoystickColor(false);
    }

    private void InputController_EndTapScreen()
    {
        SetJoystickColor(false);
    }

    private void SetJoystickPosition()
    {
        mousePos = Input.mousePosition;

        joystick.position = mousePos;

        SetJoystickColor(true);
    }

    private void SetJoystickColor(bool pressed)
    {
        if (pressed)
        {
            newAlphaColor.a = opaque;

            SetWhiteColor();
        }
        else
        {
            newAlphaColor.a = transparent;

            SetWhiteColor();
        }

        color.color = newAlphaColor;
        joystickPointColor.color = newAlphaColor;
    }

    private void OnDisable()
    {
        InputController.TapScreen -= SetJoystickPosition;
        InputController.EndTapScreen -= InputController_EndTapScreen;
    }

    private void SetWhiteColor()
    {
        newAlphaColor.r = 255;
        newAlphaColor.g = 255;
        newAlphaColor.b = 255;
    }
}
