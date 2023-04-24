using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Original Cam Settings")]
    [SerializeField] private Vector3 _startPos = new Vector3(0, 0, -10);
    [SerializeField] private float _startSize = 8;

    [Header("Idle Cam Settings")]
    [SerializeField] bool bobbingEnabled;
    [SerializeField] float bobbingSpeed = 1f;
    [SerializeField] float bobbingAmount = 0.1f;

    [Header("Focus Cam Settings")]
    [SerializeField] float _animationSpeed = 5f;
    [SerializeField] float _camZoom = 1f;
    [SerializeField] float _minSize = 5.0f;
    [SerializeField] bool combatFocus;

    [Header("Camera Shake Settings")]
    [SerializeField] float shakeDuration = 0.2f;
    [SerializeField] float shakeIntensity = 0.1f;

    [Header("Combat Targets")]
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _target;

    private Camera _camera;
    private Vector3 _targetPos;
    private float _targetSize;
    private float _bobbingOffset = 0f;

    private float _shakeTime;
    private Vector3 _shakeOffset = Vector3.zero;

    void Start()
    {
        _camera = GetComponent<Camera>();
        _startPos = _camera.gameObject.transform.position;
        _startSize = _camera.orthographicSize;
    }

    void Update()
    {

        if (combatFocus && _player != null && _target != null)
        {
            CameraFocus(_player.transform.position, _target.transform.position, _camZoom);
        }
        else
        {
            CameraIdle(_startPos, _startSize);
        }

        if (_shakeTime > 0)
        {
            // Update shake offset
            _shakeOffset = Random.insideUnitSphere * shakeIntensity;

            // Apply shake offset to camera position
            transform.position += _shakeOffset;

            // Decrease shake time
            _shakeTime -= Time.deltaTime;
        }
        else
        {
            // Reset shake offset
            _shakeOffset = Vector3.zero;
        }

    }

    void CameraIdle(Vector3 pos, float size)
    {
        // Set the target position and size to the start position and size
        _targetPos = pos;
        _targetSize = size;

        // Animate camera towards target position and size
        AnimateCamera();

        // Animate camera position to bob up & down
        if (bobbingEnabled)
        {
            _bobbingOffset += Time.deltaTime * bobbingSpeed;
            float yOffset = Mathf.Sin(_bobbingOffset) * bobbingAmount;
            transform.position += new Vector3(0f, yOffset, 0f);
        }
    }

    void CameraFocus(Vector2 pos1, Vector2 pos2, float zoomAmount = 1.0f)
    {
        // Calculate the midpoint between the two positions
        Vector2 midpoint = (pos1 + pos2) / 2.0f;

        // Set the target position to the midpoint
        _targetPos = new Vector3(midpoint.x, midpoint.y, transform.position.z);

        // Calculate the distance between the two positions
        float distance = Vector2.Distance(pos1, pos2);

        // Calculate the size of the frustum that can fit both positions
        float size = distance / 2.0f;

        // Scale the size by the zoom amount
        size /= zoomAmount;

        // Clamp the size to the minimum size
        size = Mathf.Max(size, _minSize);

        // Set the target size to the calculated size
        _targetSize = size;

        // Animate camera towards target position and size
        AnimateCamera();
    }

    void AnimateCamera()
    {
        // Move the camera towards the target position and size
        transform.position = Vector3.Lerp(transform.position, _targetPos, Time.deltaTime * _animationSpeed);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _targetSize, Time.deltaTime * _animationSpeed);
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public void CameraShake(float shakeAmount)
    {
        _shakeTime = shakeDuration;
        shakeIntensity = shakeAmount;
    }


}