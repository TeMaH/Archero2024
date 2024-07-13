using UnityEngine;
using UnityEngine.EventSystems;

public class MovementComponent : MonoBehaviour
{
    private CharacterController characterController;
    public CharacterController CharacterController
    {
        get { return characterController = characterController ?? GetComponent<CharacterController>(); }
    }

    private Vector2 moveDirection;

    private Vector3 move;
    private Vector3 verticalVelocity = Vector3.zero;

    private float rotationAngle;
    private float gravity = -9.81f;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float speedMovement;

    private void Update()
    {
        move = new Vector3(moveDirection.x, 0.0f, moveDirection.y);

        verticalVelocity.y = verticalVelocity.y + gravity * Time.deltaTime;

        Vector3 newPosition = move * speedMovement * Time.deltaTime;

        CharacterController.Move(newPosition);

        Quaternion currentRotation = CharacterController.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(move);

        CharacterController.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void HandleMove(Vector2 dir)
    {
        moveDirection = dir;
    }
}
