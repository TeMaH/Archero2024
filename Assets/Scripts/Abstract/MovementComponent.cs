using UnityEngine;

public class MovementComponent : MonoBehaviour, IMovable
{
    private CharacterController characterController;
    public CharacterController CharacterController
    {
        get { return characterController = characterController ?? GetComponent<CharacterController>(); }
    }

    private Animator characterAnimator;

    private Vector2 moveDirection;

    private Vector3 move;
    private Vector3 verticalVelocity = Vector3.zero;

    private float rotationAngle;
    private float gravity = -9.81f;

    private bool setAnim = true;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float speedMovement;

    private void Start()
    {
        characterAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        move = new Vector3(moveDirection.x, 0.0f, moveDirection.y);

        verticalVelocity.y = verticalVelocity.y + gravity * Time.deltaTime;

        Vector3 newPosition = move * speedMovement * Time.deltaTime;

        if (move.magnitude > 0.0f)
        {
            if (setAnim)
            {
                characterAnimator.SetBool("isRun", true);
                setAnim = false;
            }
        }
        if (move.magnitude == 0.0f)
        {
            if (!setAnim)
            {
                characterAnimator.SetBool("isRun", false);
                setAnim = true;
            }
        }

        CharacterController.Move(newPosition);

        Quaternion currentRotation = CharacterController.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(move);

        CharacterController.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void SartMovement(Vector2 vector2)
    {
        moveDirection = vector2;
    }

    public void StopMovement()
    {
        
    }
}
