using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

   [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 12f;

    private Rigidbody rb;
    private Vector2 moveInput;

    private PlayerInputActions inputActions;
    private CameraController cameraController;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.freezeRotation = true;

        // Récupération de la caméra
        cameraController = FindFirstObjectByType<CameraController>();

        // Input System
        inputActions = new PlayerInputActions();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += _ => moveInput = Vector2.zero;
    }

    void OnEnable()
    {
        if (inputActions != null)
            inputActions.Enable();
    }

    void OnDisable()
    {
        if (inputActions != null)
            inputActions.Disable();
    }

    void FixedUpdate()
    {
        if (cameraController == null)
            return;

        float yaw = cameraController.CurrentYaw;

        // 🔹 Directions basées sur le YAW de la caméra
        Vector3 forward = Quaternion.Euler(0f, yaw, 0f) * Vector3.forward;
        Vector3 right   = Quaternion.Euler(0f, yaw, 0f) * Vector3.right;

        Vector3 moveDir = forward * moveInput.y + right * moveInput.x;

        if (moveDir.sqrMagnitude < 0.01f)
            return;

        // 🔹 Déplacement
        Vector3 newPosition = rb.position + moveDir.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        // 🔹 Rotation du joueur dans la direction du mouvement
        Quaternion targetRotation = Quaternion.LookRotation(moveDir);
        rb.rotation = Quaternion.Slerp(
            rb.rotation,
            targetRotation,
            rotationSpeed * Time.fixedDeltaTime
        );
    }
    
}
