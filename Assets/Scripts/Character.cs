using System.Collections;
using System.Collections.Generic;
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
        movementComponent.HandleMove(input);
    }

    private void OnDisable()
    {
        InputController.MoveEvent -= InputController_MoveEvent;
    }
}
