using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Position")]
    public Vector3 offset = new Vector3(0f, 2f, -5f);
    public float followSpeed = 3f;

    [Header("Rotation")]
    public float mouseSensitivity = 40f;
    public float minPitch = -40f;
    public float maxPitch = 75f;

    // 🔑 Valeur clé partagée avec le joueur
    public float CurrentYaw => yaw;

    private float yaw;
    private float pitch;

    private PlayerInputActions inputActions;
    private Vector2 lookInput;

    private Rigidbody targetRb;
    private Vector2 smoothLookVelocity;

    void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += _ => lookInput = Vector2.zero;
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        if (inputActions != null)
            inputActions.Disable();
    }

    void LateUpdate()
    {
        if (target == null)
        {
            TryFindPlayer();
            return;
        }

        // lissage du delta souris
        lookInput.x = Mathf.SmoothDamp(lookInput.x, lookInput.x, ref smoothLookVelocity.x, 0.05f);
        lookInput.y = Mathf.SmoothDamp(lookInput.y, lookInput.y, ref smoothLookVelocity.y, 0.05f);

        // 🔹 Rotation caméra (souris)
        yaw += lookInput.x * mouseSensitivity * Time.deltaTime;
        pitch -= lookInput.y * mouseSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 targetPosition = targetRb != null
            ? targetRb.position
            : target.position;

        Vector3 desiredPosition = targetPosition + rotation * offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            1f - Mathf.Exp(-followSpeed * Time.deltaTime)
        );
        transform.rotation = rotation;
        //transform.LookAt(targetPosition + Vector3.up * 1.5f);

        /*
        // 🔹 Position caméra
        Vector3 desiredPosition = target.position + rotation * offset;
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            1f - Mathf.Exp(-followSpeed * Time.deltaTime)
        );

        // 🔹 Orientation caméra
        transform.LookAt(target.position + Vector3.up * 1.5f);
        */
    }

    void TryFindPlayer()
    {
        if (PlayerManager.Instance == null) return;

        var player = PlayerManager.Instance.GetPlayer();
        if (player != null)
        {
            target = player.transform;
            targetRb = player.GetComponent<Rigidbody>();

            // Synchronisation initiale
            yaw = target.eulerAngles.y;
        }
    }
}
