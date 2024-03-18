using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float walkSpeeds = 3.0f;
    [SerializeField] float sprintMultiplier = 3.0f;

    private PlayerInputHandle inputHandler;
    private Vector3 currentMovement;

    [Header("Rotation")]
    float currentVelocity;
    [SerializeField] private float smoothTime;


    private void Awake()
    {
        inputHandler = PlayerInputHandle.Instance;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (inputHandler.MoveInput.sqrMagnitude == 0) return;
        float speed = walkSpeeds * (inputHandler.sprintValue > 0 ? sprintMultiplier : 1f);
        currentMovement = new Vector3(inputHandler.MoveInput.x, 0f, inputHandler.MoveInput.y);
        RotatePlayer();

        transform.position = Vector3.MoveTowards(transform.position, transform.position + currentMovement, speed * Time.deltaTime);
    }

    private void RotatePlayer()
    {
        var targetAngle = Mathf.Atan2(currentMovement.x, currentMovement.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }
}
