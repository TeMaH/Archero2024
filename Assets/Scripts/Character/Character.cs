using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] MovementComponent movementComponent;

    private void OnEnable()
    {
        InputController.MoveEvent += InputController_MoveEvent; ;
    }

    private void InputController_MoveEvent(Vector2 input)
    {
        movementComponent.SartMovement(input);
    }

    private void OnDisable()
    {
        InputController.MoveEvent -= InputController_MoveEvent;
    }
}
