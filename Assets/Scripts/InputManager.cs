using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [SerializeField]
    InputActionAsset PlayerInput;
    InputAction moveAction;

    [SerializeField]
    Snake snake;

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;

        PlayerInput.Enable();
        moveAction = PlayerInput.FindAction("Move");
    }

    void Start()
    {
        moveAction.started += OnMoveStarted;
    }

    void OnDestroy()
    {
        PlayerInput.Disable();
    }

    void OnMoveStarted(InputAction.CallbackContext ctx)
    {
        var temp = ctx.ReadValue<Vector2>();
        snake.UpdateDirection(new Vector2Int((int)temp.x, (int)temp.y));

    }
}
