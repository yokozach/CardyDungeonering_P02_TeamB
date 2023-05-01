using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{

    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent onStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent onEndTouch;
    public bool _touched = false;

    private TouchManager touchControls;

    public static InputManager instance;

    public delegate void Swipe(Vector2 direction);
    public event Swipe swipePerformed;

    [SerializeField] private InputAction position, press;

    [SerializeField] private float swipeRes = 100;
    private Vector2 initialPos;
    private Vector2 currentPos => position.ReadValue<Vector2>();

    private void Awake()
    {
        touchControls = new TouchManager();
        position.Enable();
        press.Enable();
        press.performed += _ => { initialPos = currentPos; };
        press.canceled += _ => DetectSwipeMethod();
        instance = this;
    }

    private void OnEnable()
    {
        touchControls.Enable();

    }

    private void OnDisable()
    {
        touchControls.Disable();
        
    }

    private void Start()
    {
        touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);

    }

    private void Update()
    {
        
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        //Debug.Log("Touch Started " + touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        if (onStartTouch != null) onStartTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        _touched = true;
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        //Debug.Log("Touch Ended " + touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        if (onEndTouch != null) onEndTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
        _touched = false;
    }

    private void DetectSwipeMethod()
    {
        Vector2 delta = currentPos - initialPos;
        Vector2 direction = Vector2.zero;

        // Detects which axis of the swipe is bigger and alters direction to be up, down, left, or right
        if (Mathf.Abs(delta.x) > swipeRes && Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            direction.x = Mathf.Clamp(delta.x, -1, 1);
            direction.y = 0;
        }
        else if (Mathf.Abs(delta.y) > swipeRes && Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
        {
            direction.y = Mathf.Clamp(delta.y, -1, 1);
            direction.x = 0;
        }

        if (direction != Vector2.zero & swipePerformed != null)
            swipePerformed(direction);

    }

}
