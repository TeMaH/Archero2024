using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour, GameInput.IGamePlayActions
{
    private GameInput gameInput;
    private GameInput.GamePlayActions gamePlayActions;



    public static event Action<Vector2> MoveEvent;

    private void OnEnable()
    {
        if(gameInput == null)
        {
            gameInput = new GameInput();

            gameInput.Enable();

            gameInput.GamePlay.SetCallbacks(this);
        }
    }

    private void OnDisable()
    {
        if (gameInput != null)
        {
            gameInput.Disable();

            gameInput.GamePlay.RemoveCallbacks(this);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }
}
