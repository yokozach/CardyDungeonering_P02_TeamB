using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{

    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent onStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent onEndTouch;

    private TouchControls touchControls;

    private void Awake()
    {
        touchControls = new TouchControls();

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

    private void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch Started " + touchControls.Touch.ToucPosition.ReadValue<Vector2>());
        if (onStartTouch != null) onStartTouch(touchControls.Touch.ToucPosition.ReadValue<Vector2>(), (float)context.startTime);
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch Ended " + touchControls.Touch.ToucPosition.ReadValue<Vector2>());
        if (onEndTouch != null) onEndTouch(touchControls.Touch.ToucPosition.ReadValue<Vector2>(), (float)context.time);
    }


}
