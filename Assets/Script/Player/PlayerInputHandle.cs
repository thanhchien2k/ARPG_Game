using Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandle : Singleton<PlayerInputHandle>
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string look = "Look";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string sprint = "Sprint";

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction sprintAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpTrigger { get; private set; }
    public float sprintValue { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
        RegisterAction();
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
    }

    private void RegisterAction()
    {
        moveAction.performed += contex => MoveInput = contex.ReadValue<Vector2>();
        moveAction.canceled += contex => MoveInput = Vector2.zero;

        lookAction.performed += contex => LookInput = contex.ReadValue<Vector2>();
        lookAction.canceled += contex => LookInput = Vector2.zero;

        jumpAction.performed += contex => JumpTrigger = true;
        jumpAction.canceled += contex => JumpTrigger = false;

        sprintAction.performed += contex => sprintValue = contex.ReadValue<float>();
        sprintAction.canceled += contex => sprintValue = 0f;
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
    }
}
