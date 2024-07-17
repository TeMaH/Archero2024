using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour, GameInput.IGamePlayActions, GameInput.IUIJoystickActions
{
    private GameInput gameInput;
    private GameInput.GamePlayActions gamePlayActions;

    public static event Action<Vector2> MoveEvent;
    public static event Action TapScreen;
    public static event Action EndTapScreen;

    private void OnEnable()
    {
        if(gameInput == null)
        {
            gameInput = new GameInput();

            gameInput.Enable();

            gameInput.GamePlay.SetCallbacks(this);
            gameInput.UIJoystick.SetCallbacks(this);
        }
    }

    private void OnDisable()
    {
        if (gameInput != null)
        {
            gameInput.Disable();

            gameInput.GamePlay.RemoveCallbacks(this);
            gameInput.UIJoystick.RemoveCallbacks(this);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnTapScreen(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            TapScreen?.Invoke();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            EndTapScreen?.Invoke();
        }
    }
}
