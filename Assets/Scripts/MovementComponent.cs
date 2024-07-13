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

        var rotatedMovement = Quaternion.Euler(1f, 1f, 1f) * move.normalized;
        var verticalMovement = Vector3.up * verticalVelocity.y;

        if (move.magnitude > 0.0f)
        {
            rotationAngle = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
        }

        CharacterController.Move((verticalMovement + rotatedMovement * speedMovement) * Time.deltaTime);

        Quaternion currentRotation = CharacterController.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);

        CharacterController.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void HandleMove(Vector2 dir)
    {
        moveDirection = dir;
    }
}
